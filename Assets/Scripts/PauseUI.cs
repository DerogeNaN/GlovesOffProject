using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Canvas UICanvas;
    [SerializeField] GameObject returnToMenu;
    [SerializeField] EventSystem eventSystem;
    Image pauseImage;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pauseImage = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Image>();
        pauseImage.enabled = false;
    }

    public void OnGamePause()
    {
        if (gameManager.Paused)
        {
            pauseImage.enabled = true;
            returnToMenu.gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(returnToMenu);
            return;
        }
        pauseImage.enabled = false;
        returnToMenu.gameObject.SetActive(false);
    }
}
