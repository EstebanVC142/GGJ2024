using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muertos : MonoBehaviour
{
    public static Muertos singleton;
	public GameObject[] objetos;
	private void Awake()
	{
		singleton = this;
	}

	public void Activar(int cual)
	{
		for (int i = 0; i < objetos.Length; i++)
		{
			objetos[i].SetActive(i == cual);
		}
	}

	public void Desactivar()
	{
		for (int i = 0; i < objetos.Length; i++)
		{
			objetos[i].SetActive(false);
		}
	}

    public void DesactivarConDelay(float seconds = 0)
	{
		Invoke("Desactivar", seconds);
	}
}
