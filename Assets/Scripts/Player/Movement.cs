using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float lookSensibility;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private AttackBehaviour attackBehaviour;
    [SerializeField]
    private Animator anim;

    private void FixedUpdate()
    {
        if (!attackBehaviour.isAttacking)
        Move();
    }

    private void Move()
    {
        Vector2 movementAxis = input.actions["Move"].ReadValue<Vector2>();
        Vector2 lookAxis = input.actions["Look"].ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(movementAxis.x, 0, movementAxis.y);
        Quaternion ra = transform.rotation;

        if (movementDirection.sqrMagnitude > 0)
        {
            transform.forward = movementDirection.normalized;
            transform.rotation = Quaternion.Lerp(ra, transform.rotation, 0.2f);
        }
        
        if (input.actions["Run"].IsPressed())
            rb.velocity = transform.forward * movementDirection.magnitude * (velocity * 2) + Vector3.up * rb.velocity.y;
        else
            rb.velocity = transform.forward * movementDirection.magnitude * velocity + Vector3.up * rb.velocity.y;

    }
}
