using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemigoGallina : EstadosAnimal
{
    private NavMeshAgent agente;
    public Animator animaciones;
    void Awake()
    {
        base.Awake();
        agente = GetComponent<NavMeshAgent>();
    }
    public override void EstadoIdle()
    {
        base.EstadoIdle();
        if (animaciones != null) animaciones.SetFloat("Velocidad", 0);
        if (!vivo) return;
        agente.SetDestination(transform.position);
    }

    public override void EstadoSeguir()
    {
        base.EstadoSeguir();
        if (animaciones != null) animaciones.SetFloat("Velocidad", 1);
        if (!vivo) return;
        agente.SetDestination(transform.position + (transform.position - target.position) * 3);
    }

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        if (animaciones != null) animaciones.SetFloat("Velocidad", 1);
        if (!vivo) return;
        agente.SetDestination(transform.position + (transform.position - target.position) * 3);
    }

    public override void EstadoMuerto()
    {
        if (gameObject.activeSelf)
        {
            base.EstadoMuerto();
            if (animaciones != null) animaciones.SetBool("Vivo", false);
            agente.enabled = false;
        }
    }

    [ContextMenu("Matar")]
    public void Matar()
    {
        CambiarEstado(Estados.muerto);
        Invoke("desactivarse", 1);
    }

    private void desactivarse()
    {
        gameObject.SetActive(false);
    }
}
