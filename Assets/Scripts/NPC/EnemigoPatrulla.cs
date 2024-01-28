using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoPatrulla : EstadosAnimal
{
    private NavMeshAgent agente;
    public Animator animaciones;
    public Transform[] CheckPoints;
    private int indice;
    public float distanciaCheckpoints;
    private float distanciaCheckpoints2;
    public float daño = 3;
    public Collider atackCollider;

    void Awake()
    {
        base.Awake();
        agente = GetComponent<NavMeshAgent>();
        distanciaCheckpoints2 = distanciaCheckpoints * distanciaCheckpoints;
    }

    public override void EstadoIdle()
    {
        if (animaciones != null) animaciones.SetFloat("Velocidad", 1);
        if (animaciones != null) animaciones.SetBool("Atacando", false);

        agente.SetDestination(CheckPoints[indice].position);
        if ((CheckPoints[indice].position - transform.position).sqrMagnitude < distanciaCheckpoints2)
        {
            indice = (indice + 1) % CheckPoints.Length;
        }
        agente.speed = 2f;
        base.EstadoIdle();
    }

    public override void EstadoSeguir()
    {
        if (animaciones != null) animaciones.SetFloat("Velocidad", 2);
        if (animaciones != null) animaciones.SetBool("Atacando", false);
        if (!vivo) return;
        agente.SetDestination(target.position);
        agente.speed = 4f;
        base.EstadoSeguir();
    }

    public override void EstadoAtacar()
    {
        if (animaciones != null) animaciones.SetFloat("Velocidad", 0);
        if (animaciones != null) animaciones.SetBool("Atacando", true);
        if (!vivo) return;
        atackCollider.enabled = true;
        agente.SetDestination(transform.position);
        transform.LookAt(target, Vector3.up);

        base.EstadoAtacar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Perro.singleton.vida.CausarDaño(daño);
        }
    }

    public override void CambiarEstado(Estados e)
    {
        base.CambiarEstado(e);
        if (e != Estados.atacar)
        {
            atackCollider.enabled = false;
        }
    }

    public override void EstadoMuerto()
    {
        base.EstadoMuerto();
        if (animaciones != null) animaciones.SetBool("Vivo", false);
        agente.enabled = false;
    }

    [ContextMenu("Matar")]
    public void Matar()
    {
        CambiarEstado(Estados.muerto);
    }

    public void Atacar()
    {
        Perro.singleton.vida.CausarDaño(daño);
    }
}
