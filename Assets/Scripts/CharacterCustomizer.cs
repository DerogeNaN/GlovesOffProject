using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCustomizer : MonoBehaviour
{
    [SerializeField] GameObject[] hatArray = new GameObject[4];
    PlayerController playerController;
    int curHatIndex = 0;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.EnableHatSelectorMap();
    }


    public void IterateLeft(InputAction.CallbackContext callback)
    {
        if (curHatIndex == 0)
        {
            hatArray[curHatIndex].gameObject.SetActive(false);
            curHatIndex = 4;
            hatArray[curHatIndex].gameObject.SetActive(true);
        }
        else
        {
            hatArray[curHatIndex].gameObject.SetActive(false);
            curHatIndex--;
            hatArray[curHatIndex].gameObject.SetActive(true);
        }
    }

    public void IterateRight(InputAction.CallbackContext callback)
    {
        if (curHatIndex == 4)
        {
            hatArray[curHatIndex].gameObject.SetActive(false);
            curHatIndex = 0;
            hatArray[curHatIndex].gameObject.SetActive(true);
        }
        else
        {
            hatArray[curHatIndex].gameObject.SetActive(false);
            curHatIndex++;
            hatArray[curHatIndex].gameObject.SetActive(true);
        }
    }
}
