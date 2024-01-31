using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogoInicial : MonoBehaviour
{
    [Header("ImagenesIniciales")]
    public Sprite[] imagenesEscena;
    public Sprite[] imagenesTexto;
    private int indice = 0;
    public Image imagenEscena;
    public Image imagenTexto;

    private void Start()
    {
        imagenEscena.sprite = imagenesEscena[indice];
        imagenTexto.sprite = imagenesTexto[indice];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            indice++;
            if (indice >= imagenesEscena.Length)
            {
                indice = 0;
                Jugar();
            }
            else
            {
                imagenEscena.sprite = imagenesEscena[indice];
                imagenTexto.sprite = imagenesTexto[indice];
            }
            
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Mundo");
    }
}
