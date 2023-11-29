using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Canvas mainMenuUI = null;
    [SerializeField] Canvas optionsMenuUI = null;
    [SerializeField] Canvas creditsUI = null;
    [SerializeField] Canvas levelSelectUI = null;

    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject optionsFirst, creditsFirst, levelSelectFirst, mainMenuFirst;

    public void LoadLevelSelect()
    {
        eventSystem.SetSelectedGameObject(levelSelectFirst);

        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
        levelSelectUI.gameObject.SetActive(true);
    }

    public void LoadCharacterSelect()
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

    public void ExitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}