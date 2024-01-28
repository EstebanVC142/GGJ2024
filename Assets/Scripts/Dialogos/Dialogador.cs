using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogador : MonoBehaviour
{
    public int estadoActual = 0;
    public EstadoDialogo[] estados;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(ControlDialogos.singleton.teclaInicioDialogo) || Input.GetKeyDown(ControlDialogos.singleton.teclaInicioDialogo2))
            {
                StartCoroutine(ControlDialogos.singleton.Decir(estados[estadoActual].frases));
            }
        }
    }
}
