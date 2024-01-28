using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoPosible : MonoBehaviour
{
    public int tipoObjetivo;

    void Start()
    {
        Olfateo.singleton.Registrar(transform, tipoObjetivo);
    }
}
