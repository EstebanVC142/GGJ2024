using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DialogoInicial : MonoBehaviour
{
    [Header("ImagenesIniciales")]
    public Sprite[] imagenesEscena;
    public Sprite[] imagenesTexto;
    private int indice = 0;
    public Image imagenEscena;
    public Image imagenTexto;
    public PlayerInput input;

    private void Start()
    {
        imagenEscena.sprite = imagenesEscena[indice];
        imagenTexto.sprite = imagenesTexto[indice];
    }

    void Update()
    {
        if (input.actions["action"].WasPressedThisFrame())
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
        SceneManager.LoadScene("Morion3");
    }
}
