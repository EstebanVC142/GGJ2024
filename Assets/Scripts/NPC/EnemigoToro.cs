using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoToro : EstadosAnimal
{
    [SerializeField]
    private Animator anim;

    public float dir;
    public Vector2 rangoPatearIdle;
    public Vector2 rangoAtaque;
    private float tiempoAtaque;
    public NavMeshAgent nav;
    private float patearIdle;
    public Collider[] colDa�os;
    public float da�o = 1;

    public override void EstadoAtacar()
    {
        base.EstadoAtacar();
        nav.SetDestination(transform.position);
        //anim.SetFloat("Velocidad", nav.velocity.sqrMagnitude);

        dir = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);

        if (Time.time > tiempoAtaque)
        {
            if (dir > 0.7f)
            {
                anim.SetFloat("Direccion", 0);
                anim.SetTrigger("Atacar");
                colDa�os[0].enabled = true;
                Invoke("DesactivarColliders", 1);
            }
            else if (dir < -0.7f)
            {
                anim.SetFloat("Direccion", 1);
                anim.SetTrigger("Atacar");
                colDa�os[1].enabled = true;
                Invoke("DesactivarColliders", 1);
            }
            else
            {
                StartCoroutine(Mirar());

            }
            tiempoAtaque = Time.time + Random.Range(rangoAtaque.x, rangoAtaque.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Perro.singleton.vida.CausarDa�o(da�o);
        }
    }

    public override void CambiarEstado(Estados e)
    {
        base.CambiarEstado(e);
        switch (e)
        {
            case Estados.idle:
                break;
            case Estados.seguir:
                break;
            case Estados.atacar:
                anim.SetFloat("Velocidad", 0);
                print("fren�");
                break;
            case Estados.muerto:
                break;
            default:
                break;
        }
    }

    public override void EstadoIdle()
    {
        base.EstadoIdle();

        if (Time.time > patearIdle)
        {
            anim.SetTrigger("PatearIdle");
            patearIdle = Time.time + Random.Range(rangoPatearIdle.x, rangoPatearIdle.y);
        }

    }

    public override void EstadoMuerto()
    {
        base.EstadoMuerto();
    }
    public override void EstadoSeguir()
    {
        //if (estado != Estados.seguir) return;
        nav.SetDestination(target.position);
        anim.SetFloat("Velocidad", nav.velocity.sqrMagnitude);
        print("velocidad");
        base.EstadoSeguir();
    }

    public IEnumerator Mirar()
    {
        Quaternion r1 = transform.rotation;
        transform.LookAt(target.position);
        Quaternion r2 = transform.rotation;
        anim.SetFloat("Velocidad", 1);

        float t = 0;
        while (t < 1)
        {
            transform.rotation = Quaternion.Lerp(r1, r2,t);
            yield return null;
            t += Time.deltaTime;
        }
        anim.SetFloat("Velocidad", 0);
    }

    public void DesactivarColliders()
    {
        colDa�os[0].enabled = false;
        colDa�os[1].enabled = false;
    }
}
