using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    GameManager gameManager;
    Canvas UICanvas;
    Image paused;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        UICanvas = GameObject.FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (gameManager.Paused)
        {
            paused.enabled = true;
            return;
        }
        paused.enabled = false;
    }
}
