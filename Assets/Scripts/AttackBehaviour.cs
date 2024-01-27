using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject attacker;
    [SerializeField]
    private GameObject lookReference;
    [SerializeField]
    private float attackVelocity = 5;
    [SerializeField]
    private float waitTime = 1;
    [SerializeField]
    private float rotationVelocity = 10;
    [SerializeField]
    private float maxDetectionDistance = 8;
    [SerializeField]
    private int damage = 1;
    public AnimationCurve curve;
    public float avanzar;
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private LayerMask layer;

    public bool isAttacking = false;
    private Vector3 initialPosition;
    private Vector3 attackHitBoxPos;
    private bool isStarted;

    private void Awake()
    {
        initialPosition = attacker.transform.localPosition;
    }

    private void Start()
    {
        isStarted = true;
    }

    private IEnumerator Atacando()
    {
        float t = 0;
        isAttacking = true;
        Transform closestEnemy = null;
        foreach (var enemy in FindObjectsOfType<DamageReceiver>())
        {
            Vector3 enemyDistance = enemy.transform.position - transform.position;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float distance = enemyDistance.magnitude;
            Vector3 enemyDistanceNormalized = enemyDistance.normalized;
            float dot = Vector3.Dot(forward, enemyDistanceNormalized);

            if (distance < maxDetectionDistance && dot > 0.96f)
            {
                closestEnemy = enemy.transform;
            }
        }

        Vector3 lookDirection = new Vector3(); 
        if (closestEnemy != null)
        {
            lookDirection = (closestEnemy.position - transform.position).normalized;
        }

        Vector3 ffg = attacker.transform.forward;
        bool damageDealed = false;

        while (t < waitTime)
        {
            attacker.transform.localPosition = Vector3.forward * curve.Evaluate(t / waitTime);
            if (closestEnemy != null)
            attacker.transform.forward = Vector3.Lerp(ffg, lookDirection, curve.Evaluate(t / waitTime));

            
            if (t >= waitTime / 2 && !damageDealed)
            {
                damageDealed = true;
                DealDamage();
            }
            t += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isAttacking = false;
        attacker.transform.localPosition = initialPosition;
    }

    private void DealDamage()
    {
        Debug.Log("se llama");
        attackHitBoxPos = (attacker.transform.forward * 1f) + new Vector3(attacker.transform.position.x, attacker.transform.position.y, attacker.transform.position.z);
        Collider[] colliders = Physics.OverlapBox(attackHitBoxPos, attacker.transform.localScale / 4, Quaternion.identity, layer);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.GetComponent<ITakeDamage>() != null)
                {
                    colliders[i].gameObject.GetComponent<ITakeDamage>().TakeDamage(damage);
                }
            }
        }

    }

    private void Update()
    {
        if (input.actions["Attack"].WasPressedThisFrame() && !isAttacking)
        {
            StartCoroutine(Atacando());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (isAttacking)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawCube(attackHitBoxPos, transform.localScale / 2);
    }
}
