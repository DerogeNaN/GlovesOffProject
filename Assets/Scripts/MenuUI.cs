using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Canvas mainMenuUI = null;
    [SerializeField] Canvas optionsMenuUI = null;
    [SerializeField] Canvas creditsUI = null;
    [SerializeField] Canvas levelSelectUI = null;
    [SerializeField] Canvas controllerUI = null;
    [SerializeField] Canvas keyboardUI = null;

    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject optionsFirst, creditsFirst, levelSelectFirst, mainMenuFirst, controllerFirst, keyboardFirst;

    [SerializeField] TextMeshProUGUI screenText;

    #region LoadCanvas
    public void LoadLevelSelect()
    {
        eventSystem.SetSelectedGameObject(levelSelectFirst);

        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(true);
    }

    public void LoadOptionsMenu()
    {
        eventSystem.SetSelectedGameObject(optionsFirst);

        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(true);
        levelSelectUI.gameObject.SetActive(false);
        controllerUI.gameObject.SetActive(false);
        keyboardUI.gameObject.SetActive(false);
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

    public void LoadController()
    {
        eventSystem.SetSelectedGameObject(controllerFirst);

        mainMenuUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(false);
        controllerUI.gameObject.SetActive(true);
    }

    public void LoadKeyboard()
    {
        eventSystem.SetSelectedGameObject(keyboardFirst);

        mainMenuUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(false);
        keyboardUI.gameObject.SetActive(true);
    }
    #endregion

    #region LoadScenes
    public void LoadTempleScene()
    {
        SceneManager.LoadScene(sceneName: "TestingSceneTemple");
    }

    public void LoadShipScene()
    {
        SceneManager.LoadScene(sceneName: "Ship_Scene");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    #endregion

    public void FullScreenToggle()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        screenText.text = "FULL SCREEN";
    }

    public void WindowedToggle()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        screenText.text = "WINDOWED";
    }

    public void ExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}