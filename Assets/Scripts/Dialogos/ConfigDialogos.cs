using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Archivo Config")]
public class ConfigDialogos : ScriptableObject  
{
    [Header("General")]
    public float tiempoLetra = 0.1f;
    public PersonajeDialogo[] personajes;
        
    [Header("Teclas")]
    public KeyCode teclaSkip = KeyCode.Space;
    public KeyCode teclaSkip2 = KeyCode.Joystick1Button5;

    public KeyCode teclaSiguienteFrase;
    public KeyCode teclaInicioDialogo = KeyCode.B;
    public KeyCode teclaInicioDialogo2 = KeyCode.Joystick1Button3;
}
