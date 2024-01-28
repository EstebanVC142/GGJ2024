using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlDialogos : MonoBehaviour
{

    // Singleton

    public static ControlDialogos singleton;
    public GameObject dialogo;
    public Text txtDialogo;
    public Image imCara;
    [Header("Configuracion de teclado")]
    public ConfigDialogos configuracion;
    [Header("Ensayos")]
    public Frase[] dialogoEnsayo;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogo.SetActive(false);
    }

    public IEnumerator Decir(Frase[] _dialogo)
    {
        dialogo.SetActive (true);
        for (int i = 0; i < _dialogo.Length; i++)
        {
            txtDialogo.text = "";
            imCara.sprite = configuracion.personajes[_dialogo[i].personaje].GetCara(0);
            for (int j = 0; j < _dialogo[i].texto.Length + 1; j++)
            {
                yield return new WaitForSeconds(configuracion.tiempoLetra);
                if ( Input.GetKey(configuracion.teclaSkip) || Input.GetKey(configuracion.teclaSkip2))
                {
                    j = _dialogo[i].texto.Length;
                }
                txtDialogo.text = _dialogo[i].texto.Substring(0, j);
                if (j < _dialogo[i].texto.Length) imCara.sprite = configuracion.personajes[_dialogo[i].personaje].GetCara(ArreglarLetra(_dialogo[i].texto[j].ToString()));
             
            }
            txtDialogo.text = _dialogo[i].texto;
            yield return new WaitForSeconds(0.1f);
            yield return new WaitUntil(() => Input.GetKeyUp(configuracion.teclaSiguienteFrase));
        }
        dialogo.SetActive(false);

    }

    [ContextMenu("Activar prueba")]

    public void Prueba()
    {
        StartCoroutine(Decir(dialogoEnsayo));
    }
    public string ArreglarLetra(string letra)
    {
        string resultado = letra.ToUpper();
        resultado = resultado.Replace("Á", "A");
        resultado = resultado.Replace("É", "A");
        resultado = resultado.Replace("Í", "A");
        resultado = resultado.Replace("Ó", "A");
        resultado = resultado.Replace("Ú", "A");
        return resultado;
    }
}


[System.Serializable]

public class Frase
{
    public string texto;
    public int personaje;
}   

[System.Serializable]
public class EstadoDialogo
{
    public Frase[] frases;
}

[System.Serializable]
public class CaraDialogo
{
    public Sprite cara;
    public string letra;
}

[System.Serializable]
public class PersonajeDialogo
{
    public CaraDialogo[] caras;
    public Sprite GetCara(string l)
    {
        int indice = 0;
        for (int i = 0; i < caras.Length; i++)
        {
            if (caras[i].letra == l)
            {
                indice = i;
                break;
            }
        }
        return caras[indice].cara;
    }

    public Sprite GetCara(int i)
    {
        i = Mathf.Clamp(i, 0, caras.Length - 1);
        return caras[i].cara;
    }
}