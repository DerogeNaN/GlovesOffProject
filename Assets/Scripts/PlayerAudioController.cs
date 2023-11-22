using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] AudioSource soundFXSource, vocalSource, miscSource;

    public void SFXPlay(AudioClip soundFXClip)
    {
        soundFXSource.clip = soundFXClip;
        soundFXSource.Play();
        Debug.Log("SFX");
    }

    public void VocalPlay(AudioClip vocalClip)
    {
        vocalSource.clip = vocalClip;
        vocalSource.Play();
        Debug.Log("Vocals");
    }

    public void MiscPlay(AudioClip miscClip)
    {
        miscSource.clip = miscClip;
        miscSource.Play();
        Debug.Log("Misc. Sound");
    }
}
