using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Perro : MonoBehaviour
{
    public static Perro singleton;
    public Animator animator;
    public Movement movement;
    public AttackBehaviour attack;
    private float velocidad;
    private Vector3 posicionAnterior;
    public GameObject panelMuerte;
    public GameObject botonReiniciar;

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

    public void Morir()
    {
        if (!movement.vivo) return;
        panelMuerte.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(botonReiniciar);
        animator.SetBool("vivo", false);
        movement.vivo = false; 
        attack.vivo = false;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Salir()
    {
        Application.Quit();
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
