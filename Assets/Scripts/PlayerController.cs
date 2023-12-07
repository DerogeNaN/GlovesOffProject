using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerStamina playerStamina;
    CharacterCustomizer characterCustomizer;
    [SerializeField] GameManager gameManager;
    PlayerSpawner spawner;
    string previousActionMap;
    public bool controlSchemeKeyboard;

    Singleplayer singleplayer;

    public enum Actions
    {
        None,
        Block,
        Kick,
        Punch
    }
    Actions chosenAction = Actions.None;

    public Actions ChosenAction { get { return chosenAction; } }

    public bool newActionChosen;

    private void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
        playerInput = GetComponent<PlayerInput>();
        characterCustomizer = GetComponent<CharacterCustomizer>();
        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<PlayerSpawner>();
        singleplayer = FindObjectOfType<Singleplayer>();
        if (spawner._players[0] != null && spawner._players[0] != gameObject && singleplayer.singleplayer)
        {
            return;
        }

        playerInput.actions.FindAction("Punch").performed += Punch;
        playerInput.actions.FindAction("Kick").performed += Kick;
        playerInput.actions.FindAction("Block").performed += Block;

        playerInput.actions.FindAction("WagerUp").performed += WagerUp;
        playerInput.actions.FindAction("WagerDown").performed += WagerDown;

        playerInput.actions.FindAction("Ready").performed += Ready;
        playerInput.actions.FindAction("Left").performed += characterCustomizer.IterateLeft;
        playerInput.actions.FindAction("Right").performed += characterCustomizer.IterateRight;

        playerInput.actions.FindAction("Pause").performed += Pause;
        playerInput.actions.FindAction("Resume").performed += Resume;
    }

    void LateUpdate()
    {
    }
    #region HatSelection
    public void Ready(InputAction.CallbackContext callback)
    {
        spawner.ReadyUp(playerInput.playerIndex);
    }
    #endregion
    #region RPS
    void Punch(InputAction.CallbackContext callback)
    {
        chosenAction = Actions.Punch;
        newActionChosen = true;
        // Testing
        Debug.Log(playerInput.playerIndex.ToString() + " Punch Selected");
    }
    
    void Kick(InputAction.CallbackContext callback)
    {
        chosenAction = Actions.Kick;
        newActionChosen = true;
        // Testing
        Debug.Log(playerInput.playerIndex.ToString() + " Kick Selected");
    }

    void Block(InputAction.CallbackContext callback)
    {
        chosenAction = Actions.Block;
        newActionChosen = true;
        // Testing
        Debug.Log(playerInput.playerIndex.ToString() + " Block Selected");
    }

    public void RandomiseAction()
    {
        chosenAction = (Actions)Random.Range(1, 3);
        // Testing
        Debug.Log(playerInput.playerIndex.ToString() + $" Randomised Action selected: {chosenAction}");
    }
    #endregion
    #region Wager
    void WagerUp(InputAction.CallbackContext callback)
    {
        playerStamina.increaseWager();
    }

    void WagerDown(InputAction.CallbackContext callback)
    {
        playerStamina.decreaseWager();
    }
    #endregion

    private void Pause(InputAction.CallbackContext callback)
    {
        gameManager.PauseGame(callback, this);
        DisablePreviousActionMap();
    }

    private void Resume(InputAction.CallbackContext callback)
    {
        gameManager.ResumeGame(callback, this);
        EnablePreviousActionMap(); 
    }

    public void EnableHatSelectorMap()
    {
        playerInput.actions.FindActionMap("HatSelection").Enable();
        playerInput.actions.FindActionMap("InGame").Enable();
    }
    public void RPSPhase()
    {
        playerInput.actions.FindActionMap("RPS").Enable();
        playerInput.actions.FindActionMap("HatSelection").Disable();
        previousActionMap = "RPS";
    }
    public void WagerPhase()
    {
        playerInput.actions.FindActionMap("Wager").Enable();
        playerInput.actions.FindActionMap("RPS").Disable();
        previousActionMap = "Wager";
    }
    public void ActionPhase()
    {
        playerInput.actions.FindActionMap("Wager").Disable();
        playerInput.actions.FindActionMap("RPS").Disable();
        previousActionMap = null;
    }

    public void EnablePreviousActionMap()
    {
        if (previousActionMap != null)
        {
            playerInput.actions.FindActionMap(previousActionMap).Enable();
        }
    }

    public void DisablePreviousActionMap()
    {
        if (previousActionMap != null)
        {
            playerInput.actions.FindActionMap(previousActionMap).Disable();
        }
    }

    public void ResetChosenAction()
    {
        chosenAction = Actions.None;
    }
}