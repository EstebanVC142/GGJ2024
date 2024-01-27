using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    public AnimationCurve curve;
    public float avanzar;
    [SerializeField]
    private PlayerInput input;

    public bool isAttacking = false;
    private Vector3 initialPosition;
    private quaternion initialRotation;

    private void Awake()
    {
        initialPosition = attacker.transform.localPosition;
        initialRotation = attacker.transform.rotation;
    }

    /*public void Attack(int damage)
    {
        if (isAttacking) return;

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

        if (closestEnemy != null)
        {
            attacker.transform.LookAt(closestEnemy);
        }
        StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        isAttacking = true;
        Coroutine forward = StartCoroutine(MeshMovement(true));
        Coroutine rotation;
        yield return new WaitForSeconds(waitTime);
        if (forward != null)
        {
            StopCoroutine(forward);
            forward = StartCoroutine(MeshMovement(false));
        }
        yield return new WaitForSeconds(waitTime);
        if (forward != null)
        {
            StopCoroutine(forward);
        }
        isAttacking = false;
        attacker.transform.localPosition = initialPosition;
    }

    private IEnumerator MeshMovement(bool forward)
    {
        while (true)
        {
            if (forward)
                attacker.transform.position += attacker.transform.forward * attackVelocity * Time.fixedDeltaTime;
            else
                attacker.transform.position -= attacker.transform.forward * attackVelocity * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }*/

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
            //attacker.transform.LookAt(closestEnemy);
            //Debug.Log(attacker.transform.rotation.eulerAngles);// = quaternion.Euler(attacker.transform.rotation.eulerAngles.x, 0, attacker.transform.rotation.eulerAngles.z);
        }

        Vector3 ffg = attacker.transform.forward;

        while (t < waitTime)
        {
            attacker.transform.localPosition = Vector3.forward * curve.Evaluate(t / waitTime);
            if (closestEnemy != null)
            attacker.transform.forward = Vector3.Lerp(ffg, lookDirection, curve.Evaluate(t / waitTime));
            t += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isAttacking = false;
        attacker.transform.localPosition = initialPosition;
        attacker.transform.localRotation = initialRotation;
        //attacker.transform.localRotation = initialRotation;
    }

    private IEnumerator BackToNormalRotation()
    {
        while (true)
        {
            attacker.transform.rotation = Quaternion.Lerp(attacker.transform.rotation, initialRotation, rotationVelocity * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update()
    {
        if (input.actions["Attack"].WasPressedThisFrame() && !isAttacking)
        {
            //Attack(0);
            StartCoroutine(Atacando());
        }
    }
}
