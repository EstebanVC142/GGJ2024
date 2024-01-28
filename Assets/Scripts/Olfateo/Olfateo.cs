using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


public class Olfateo : MonoBehaviour
{
    public Objetivo[] objetivos;
    public float distanciaOlfateo;
    public static Olfateo singleton;
    public int objetivoActual;
    public GameObject particulas;
	private void Awake()
	{
        singleton = this;
	}

    public void Registrar(Transform t, int i)
	{
        objetivos[i].Registrar(t);
	}

    public void Olfatear()
	{
        Transform encontrado = objetivos[objetivoActual].GetObjetivo(transform.position, distanciaOlfateo);
		if (encontrado != null)
		{
            GameObject huellas = Instantiate(particulas, transform.position, transform.rotation);
            huellas.transform.forward = encontrado.position - transform.position;
		}
	}

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = new Color(1,0.5f,0);
        Handles.DrawWireDisc(transform.position, Vector3.up, distanciaOlfateo);
    }
#endif 
}

[System.Serializable]
public class Objetivo
{
    public string nombre;
    public List<Transform> posibles;
    public Transform GetObjetivo(Vector3 posicion, float distanciaMaxima)
	{
		Transform tFinal = null;
        float distancia = distanciaMaxima*distanciaMaxima;
		for (int i = 0; i < posibles.Count; i++)
		{
            float d = (posibles[i].position - posicion).sqrMagnitude;
			if (d<distancia)
			{
                distancia = d;
                tFinal = posibles[i];
            }
		}

        return tFinal;

    }

    public void Registrar(Transform t)
	{
        posibles.Add(t);
	}
}
