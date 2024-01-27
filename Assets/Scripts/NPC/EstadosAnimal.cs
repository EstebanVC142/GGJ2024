using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class EstadosAnimal : MonoBehaviour
{
    public Estados estado;
    public float distaciaSeguir;
    public float distaciaAtacar;
    public float distaciaEscapar;

    public bool autoseleccionarTarget = true;
    public Transform target;
    public float distancia;


    private void Awake()
    {
        if(autoseleccionarTarget)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CalcularDistancia());
    }

    private void LateUpdate()
    {
        CheckEstado();
    }

    private void CheckEstado()
    {
        switch (estado)
        {
            case Estados.idle:
                EstadoIdle();
                break;
            case Estados.seguir:
                EstadoSeguir();
                break;
            case Estados.atacar:
                EstadoAtacar();
                break;
            case Estados.muerto:
                EstadoMuerto();
                break;
            default:
                break;
        }
    }

    public void CambiarEstado(Estados e)
    {
        switch (e)
        {
            case Estados.idle:
                break;
            case Estados.seguir:
                break;
            case Estados.atacar:
                break;
            case Estados.muerto:
                break;
            default:
                break;
        }

        estado = e;
    }

    public virtual void EstadoIdle()
    {
        if (distancia < distaciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    public virtual void EstadoSeguir()
    {
        if (distancia < distaciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    public virtual void EstadoAtacar()
    {
        if (distancia < distaciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
    }
    public virtual void EstadoMuerto()
    {
        if (distancia < distaciaSeguir)
        {
            CambiarEstado(Estados.seguir);
        }
    }

    IEnumerator CalcularDistancia()
    {
        while (true)
        {
            if (target != null)
            {
                distancia = Vector3.Distance(transform.position, target.position);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,Vector3.up, distaciaSeguir);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, distaciaAtacar);
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, Vector3.up, distaciaEscapar);
    }
#endif 

    private void OnDrawGizmos()
    {
        int icono = (int)estado;
        icono++;

        Gizmos.DrawIcon(transform.position + Vector3.up * 1.2f, "0" + icono + ".png", false);
    }
}
public enum Estados
{
    idle   = 0,
    seguir = 1,
    atacar = 2,
    muerto = 3,
}
