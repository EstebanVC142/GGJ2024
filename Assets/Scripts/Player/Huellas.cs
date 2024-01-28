using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huellas : MonoBehaviour
{
    public float tiempoVida = 5;
    public float velocidad = 10;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocidad * Time.deltaTime * Vector3.forward);
    }
}
