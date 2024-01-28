using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoConejo : EstadosAnimal

{
    private NavMeshAgent agente;
    public Animator animaciones;
    public float daño = 3;

    void Awake()
    {
        base.Awake();
        agente = GetComponent<NavMeshAgent>();
    }
    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animaciones != null) animaciones.SetFloat("Velocidad", 0);
        if (animaciones != null) animaciones.SetBool("Atacando", false);
        agente.SetDestination(transform.position);
    }

    public override void EstadoSeguir()
    {
        base.EstadoSeguir();
        if (animaciones != null) animaciones.SetFloat("Velocidad", 1);
        if (animaciones != null) animaciones.SetBool("Atacando", false);
        agente.SetDestination(transform.position + (transform.position - target.position) * 3);
    }

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        if (animaciones != null) animaciones.SetFloat("Velocidad", 1);
        if (animaciones != null) animaciones.SetBool("Atacando", false);
        agente.SetDestination(transform.position + (transform.position - target.position) * 3);
            transform.LookAt(target, Vector3.up);
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
