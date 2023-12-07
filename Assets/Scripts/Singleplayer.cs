using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Singleplayer : MonoBehaviour
{
    [SerializeField] Toggle singleplayerToggle;
    public bool singleplayer = false;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetSingleplayer()
    {
        singleplayer = singleplayerToggle.isOn;
    }
}