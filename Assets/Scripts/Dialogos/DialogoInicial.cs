using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoInicial : MonoBehaviour
{
    [Header("ImagenesIniciales")]
    public Sprite[] imagenes;
    private int indice = 0;
    public Image imagen;

    private void Start()
    {
        imagen.sprite = imagenes[indice];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            indice++;
            if (indice >= imagenes.Length)
            {
                indice = 0;
                Jugar();
            }
            imagen.sprite = imagenes[indice];
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }
}
