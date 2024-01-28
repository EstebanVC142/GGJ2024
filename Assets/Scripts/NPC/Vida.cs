using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vida : MonoBehaviour, ITakeDamage
{
    public float vidaInicial;
    public float vidaActual;
    public Animator anim;
    public UnityEvent eventoMorir;


    public void Start()
    {
        vidaActual = vidaInicial;
    }

    public void CausarDa�o(float cuanto)
    {
        vidaActual -= cuanto;
        if (vidaActual <= 0)
        {
            print("Muerto " + gameObject.name);
            eventoMorir.Invoke();
        }
        else if (anim != null)
        {
            anim.SetTrigger("da�o");
        }
    }

}
