using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Vida : MonoBehaviour, ITakeDamage
{
    public float vidaInicial;
    public float vidaActual;
    public Slider sliderVida;
    public Animator anim;
    public UnityEvent eventoMorir;


    public void Start()
    {
        vidaActual = vidaInicial;
        if (sliderVida != null)
        sliderVida.maxValue = vidaInicial;
    }

    public void CausarDaño(float cuanto)
    {
        vidaActual -= cuanto;
        if (vidaActual <= 0)
        {
            print("Muerto " + gameObject.name);
            eventoMorir.Invoke();
        }
        else if (anim != null)
        {
            anim.SetTrigger("daño");
        }
        if (sliderVida != null)
        {
            sliderVida.value = vidaActual;
        }
    }

}
