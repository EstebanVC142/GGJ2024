using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayFmod : MonoBehaviour
{
    private bool playing = false;
    private FMOD.Studio.EventInstance instance;
    public FMODUnity.StudioEventEmitter[] emitter;

  
    public void playSound(int i)
    {
        emitter[i].Play();
    }

    public void stopSound(int i)
    {
        emitter[i].Stop();
    }

    public void iniciarSonido()
    {
        if (playing == false)
        {
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/Effectos/Perro/Olfateo");
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            instance.start();
        }
            playing = true;
    }

    public void detenerSonido()
    {
        if (playing == true)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }
        playing = false;
    }


}
