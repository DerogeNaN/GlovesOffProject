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
    [SerializeField] Image pauseCover;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pauseImage = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Image>();
        pauseImage = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Image>();
        pauseImage.enabled = false;
        pauseCover.enabled = false;
    }

    public void OnGamePause()
    {
        if (gameManager.Paused)
        {
            pauseImage.enabled = true;
            pauseCover.enabled = true;
            returnToMenu.gameObject.SetActive(true);
            eventSystem.SetSelectedGameObject(returnToMenu);
            return;
        }
        pauseImage.enabled = false;
        pauseCover.enabled = false;
        returnToMenu.gameObject.SetActive(false);
    }
}
