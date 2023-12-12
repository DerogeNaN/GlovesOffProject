using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip onHover, onClick, playSound, optionsSound, quitSound1, quitSound2, quitSound3, stageSelect, templeLevel, shipLevel, desertLevel;
    [SerializeField] AudioSource menuSFX, voiceSFX;
    [SerializeField] MusicManager musicManager;
    private float globalVolume;
    private int selectQuit = 3;

    public void Update()
    {
        globalVolume = musicManager.volume.value;
    }

    public void HoverSelect()
    {
        menuSFX.clip = onHover;
        menuSFX.volume = globalVolume;
        menuSFX.Play();
    }

    public void ClickSelect()
    {
        menuSFX.clip = onClick;
        menuSFX.volume = globalVolume;
        menuSFX.Play();
    }

    public void PlaySelect()
    {
        voiceSFX.volume = globalVolume;
        voiceSFX.clip = playSound;
        voiceSFX.Play();
    }

    public void OptionsSelect()
    {
        voiceSFX.volume = globalVolume;
        voiceSFX.clip = optionsSound;
        voiceSFX.Play();
    }

    public void SelectStage()
    {
        voiceSFX.volume = globalVolume;
        voiceSFX.clip = stageSelect;
        voiceSFX.Play();
    }

    public void SelectTemple()
    {
        voiceSFX.volume = globalVolume;
        voiceSFX.clip = templeLevel;
        voiceSFX.Play();
    }

    public void SelectShip()
    {
        voiceSFX.volume = globalVolume;
        voiceSFX.clip = shipLevel;
        voiceSFX.Play();
    }

    public void SelectDesert()
    {
        voiceSFX.volume = globalVolume;
        voiceSFX.clip = desertLevel;
        voiceSFX.Play();
    }

    public void AudioClear()
    {
        voiceSFX.Stop();
    }

    public void QuitSelect()
    {
        selectQuit = Random.Range(1, 7);

        if (selectQuit == 1)
        {
            voiceSFX.clip = quitSound3;
            voiceSFX.volume = globalVolume;
        }
        else if (selectQuit == 2) 
        {
            voiceSFX.clip = quitSound2;
            voiceSFX.volume = globalVolume;
        }
        else
        {
            voiceSFX.clip = quitSound1;
            voiceSFX.volume = globalVolume;
        }

        voiceSFX.Play();
    }
}
