using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip onHover, onClick, playSound, optionsSound, quitSound1, quitSound2, quitSound3, stageSelect, templeLevel, shipLevel, desertLevel;
    [SerializeField] AudioSource menuSFX, voiceSFX;
    private int selectQuit = 3;

    public void HoverSelect()
    {
        menuSFX.clip = onHover;
        menuSFX.Play();
    }

    public void ClickSelect()
    {
        menuSFX.clip = onClick;
        menuSFX.Play();
    }

    public void PlaySelect()
    {
        voiceSFX.volume = 0.6f;
        voiceSFX.clip = playSound;
        voiceSFX.Play();
    }

    public void OptionsSelect()
    {
        voiceSFX.volume = 0.6f;
        voiceSFX.clip = optionsSound;
        voiceSFX.Play();
    }

    public void SelectStage()
    {
        voiceSFX.volume = 0.6f;
        voiceSFX.clip = stageSelect;
        voiceSFX.Play();
    }

    public void SelectTemple()
    {
        voiceSFX.volume = 0.6f;
        voiceSFX.clip = templeLevel;
        voiceSFX.Play();
    }

    public void SelectShip()
    {
        voiceSFX.volume = 0.6f;
        voiceSFX.clip = shipLevel;
        voiceSFX.Play();
    }

    public void SelectDesert()
    {
        voiceSFX.volume = 0.6f;
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
            voiceSFX.volume = 1;
        }
        else if (selectQuit == 2) 
        {
            voiceSFX.clip = quitSound2;
            voiceSFX.volume = 1;
        }
        else
        {
            voiceSFX.clip = quitSound1;
            voiceSFX.volume = 0.6f;
        }

        voiceSFX.Play();
    }
}
