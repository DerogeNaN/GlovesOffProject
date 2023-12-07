using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Canvas mainMenuUI = null, optionsMenuUI = null, creditsUI = null, levelSelectUI = null;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject optionsFirst, creditsFirst, levelSelectFirst, mainMenuFirst;
    [SerializeField] TextMeshProUGUI windowMode;

    public void LoadLevelSelect()
    {
        eventSystem.SetSelectedGameObject(levelSelectFirst);

        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(true);
    }

    public void LoadTemple()
    {
        SceneManager.LoadScene(sceneName: "Temple_Scene");
    }

    public void LoadShip()
    {
        SceneManager.LoadScene(sceneName: "Ship_Scene");
    }

    public void LoadDesert()
    {
        SceneManager.LoadScene(sceneName: "Temple_Scene");
    }

    public void LoadOptionsMenu()
    {
        eventSystem.SetSelectedGameObject(optionsFirst);

        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(true);
        levelSelectUI.gameObject.SetActive(false);
    }

    public void LoadMainMenu()
    {
        eventSystem.SetSelectedGameObject(mainMenuFirst);

        mainMenuUI.gameObject.SetActive(true);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(false);
    }

    public void LoadCredits()
    {
        eventSystem.SetSelectedGameObject(creditsFirst);

        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(true);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

    public void EnableFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        windowMode.text = "FULLSCREEN";
    }

    public void EnableWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        windowMode.text = "WINDOWED";
    }

    public void ExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}