using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackBehaviour : MonoBehaviour, IAttack
{
    [SerializeField]
    private GameObject attacker;
    [SerializeField]
    private float attackVelocity = 5;
    [SerializeField]
    private float waitTime = 1;
    [SerializeField]
    private float maxDetectionDistance = 8;
    [SerializeField]
    private PlayerInput input;

    private bool isAttacking = false;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = attacker.transform.localPosition;
    }

    public void Attack(int damage)
    {
        if (isAttacking) return;

        Transform closestEnemy = null;
        foreach (var enemy in FindObjectsOfType<DamageReceiver>())
        {
            Vector3 enemyDistance = enemy.transform.position - transform.position;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            float distance = enemyDistance.magnitude;
            float dot = Vector3.Dot(forward, enemyDistance);

            Debug.Log($"dot: {dot}");
            if (distance < maxDetectionDistance/* && dot > 2.3f && dot < 3.3f*/)
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
    }

    private void Update()
    {
        if (input.actions["Attack"].WasPressedThisFrame())
        {
            Attack(0);
        }
    }
}
