using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Canvas mainMenuUI = null;
    [SerializeField] Canvas optionsMenuUI = null;
    [SerializeField] Canvas creditsUI = null;
    [SerializeField] Canvas levelSelectUI = null;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void LoadLevelSelect()
    {
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
        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(true);
    }

    public void LoadMainMenu()
    {
        mainMenuUI.gameObject.SetActive(true);
        creditsUI.gameObject.SetActive(false);
        optionsMenuUI.gameObject.SetActive(false);
    }

    public void LoadCredits()
    {
        mainMenuUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(true);
        optionsMenuUI.gameObject.SetActive(false);
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