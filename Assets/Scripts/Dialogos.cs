using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogos : MonoBehaviour
{
    public GameObject dialogo;
    public Text txtDialogo;

    // Start is called before the first frame update
    void Start()
    {
        dialogo.SetActive(false);
    }

    public void Decir(string texto)
    {
        txtDialogo.text = texto;
        dialogo.SetActive (true);
    }

    [ContextMenu("Activar prueba")]

    public void Prueba()
    {
        Decir("Que pasa mpp?");
    }
}
