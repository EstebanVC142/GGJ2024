using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float sniffRate = 2f;
    [SerializeField]
    private float sniffProgression = 0;
    [SerializeField]
    private float sniffEnergy = 1;
    [SerializeField]
    private float sniffRecuperation = 0.2f;

    private bool blockSniff = false;

    private void Update()
    {
        if (!attackBehaviour.isAttacking)
        Move();
    }

    private void Move()
    {
        Vector2 movementAxis = input.actions["Move"].ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(movementAxis.x, 0, movementAxis.y);
        Quaternion ra = transform.rotation;

        if (movementDirection.sqrMagnitude > 0)
        {
            transform.forward = movementDirection.normalized;
            transform.rotation = Quaternion.Lerp(ra, transform.rotation, 0.2f);
        }
        anim.SetBool("correr", input.actions["Run"].IsPressed());

        if (input.actions["Run"].IsPressed())
            rb.velocity = transform.forward * movementDirection.magnitude * (velocity * 2) + Vector3.up * rb.velocity.y;
        else if (input.actions["Smell"].IsPressed() && !blockSniff)
        {
            rb.velocity = transform.forward * movementDirection.magnitude * (velocity / 2) + Vector3.up * rb.velocity.y;
            anim.SetFloat("olfateo", input.actions["Smell"].ReadValue<float>());
            sniffProgression += Time.deltaTime;
            sniffEnergy -= Time.deltaTime * sniffRecuperation;
            slider.value = sniffEnergy;
            if (sniffEnergy <= 0.3f)
            {
                blockSniff = true;
            }
            if (sniffProgression >= sniffRate)
            {
                sniffProgression = 0;
                Olfateo.singleton.Olfatear();
            }
        }
        else
        {
            rb.velocity = transform.forward * movementDirection.magnitude * velocity + Vector3.up * rb.velocity.y;
            anim.SetFloat("olfateo", 0);
            if (sniffEnergy < 1)
            {
                sniffEnergy += (Time.deltaTime * sniffRecuperation);
                slider.value = sniffEnergy;
                if (sniffEnergy >= 0.6f)
                    blockSniff = false;
            }
        }
    }
}
