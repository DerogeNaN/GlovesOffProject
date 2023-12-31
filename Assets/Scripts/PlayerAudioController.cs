using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] AudioSource soundFXSource, vocalSource, miscSource;
    [SerializeField] float soundVolume = 1f;
    [SerializeField] AudioClip oog1, oog2, oog3;
    [SerializeField] MusicManager musicManager;
    int getRandomSound;

    public void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<MusicManager>();
    }

    public void Update()
    {
        soundVolume = musicManager.volume.value;
    }

    public void ChangeGorillaVolume(float getSoundVolume)
    {
        soundFXSource.Stop();
        vocalSource.Stop();
        miscSource.Stop();
        soundVolume *= getSoundVolume;
    }

    public void RandomOgg()
    {
        getRandomSound = Random.Range(0, 2);

        switch (getRandomSound){
            case 0:
                soundFXSource.clip = oog1;
                vocalSource.volume = soundVolume;
                soundFXSource.Play();
                break;
            case 1:
                soundFXSource.clip = oog2;
                vocalSource.volume = soundVolume;
                soundFXSource.Play();
                break;
            case 2:
                soundFXSource.clip = oog3;
                vocalSource.volume = soundVolume;
                soundFXSource.Play();
                break;
        }
    }

    public void SFXPlay(AudioClip soundFXClip)
    {
        soundFXSource.clip = soundFXClip;
        soundFXSource.volume = soundVolume;
        vocalSource.volume = soundVolume;
        miscSource.volume = soundVolume;
        soundFXSource.Play();
        Debug.Log("SFX");
    }

    public void VocalPlay(AudioClip vocalClip)
    {
        vocalSource.clip = vocalClip;
        soundFXSource.volume = soundVolume;
        vocalSource.volume = soundVolume;
        miscSource.volume = soundVolume;
        vocalSource.Play();
        Debug.Log("Vocals");
    }

    public void MiscPlay(AudioClip miscClip)
    {
        miscSource.clip = miscClip;
        soundFXSource.volume = soundVolume;
        vocalSource.volume = soundVolume;
        miscSource.volume = soundVolume;
        miscSource.Play();
        Debug.Log("Misc. Sound");
    }
}
