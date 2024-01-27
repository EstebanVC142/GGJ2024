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
        lookAxis.Normalize();

        rb.velocity = transform.rotation * (new Vector3(movementAxis.x * velocity, rb.velocity.y, movementAxis.y * velocity));
        rb.rotation *= Quaternion.Euler(transform.rotation.x, lookAxis.x * lookSensibility, transform.rotation.z);
    }
}
