using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void AcercaDe()
    {
        SceneManager.LoadScene("AcercaDe");
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();

    }
}
