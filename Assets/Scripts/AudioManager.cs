using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip onHover, onClick;
    [SerializeField] AudioSource menuSFX;

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
}
