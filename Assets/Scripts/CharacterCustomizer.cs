using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomizer : MonoBehaviour
{
    GameObject[] hatArray = { };
    PlayerController playerController;
    int curHatIndex = 0;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.EnableHatSelectorMap();
    }


    void Update()
    {
        
    }

    public void IterateLeft(InputAction.CallbackContext callback)
    {
        Debug.Log("IterateLeft");
    }

    public void IterateRight(InputAction.CallbackContext callback)
    {
        Debug.Log("IterateRight");
    }
}
