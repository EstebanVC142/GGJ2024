using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 movementAxis = input.actions["Move"].ReadValue<Vector2>();
        Vector2 lookAxis = input.actions["Look"].ReadValue<Vector2>();

        rb.velocity = transform.forward * movementAxis.y * velocity;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundDistance, layer))
        {
            rb.velocity -= new Vector3(0, gravity * Time.fixedDeltaTime, 0);
        }
        else
            rb.velocity -= new Vector3(0, 0, 0);
        rb.rotation *= Quaternion.Euler(transform.rotation.x, lookAxis.x * lookSensibility * Time.fixedDeltaTime, transform.rotation.z);
    }
}
