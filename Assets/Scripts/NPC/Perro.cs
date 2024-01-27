using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Perro : MonoBehaviour
{
    public static Perro singleton;
    public Animator animator;
    private float velocidad;
    private Vector3 posicionAnterior;

    public Vida vida;

    private void Start()
    {
        posicionAnterior = transform.position;
    }

    private void FixedUpdate()
    {
        velocidad = (transform.position - posicionAnterior).sqrMagnitude;
        animator.SetFloat("velocidad", velocidad*50);
        posicionAnterior = (transform.position);
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
