using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerStamina playerStamina;
    public enum Actions
    {
        None,
        Block,
        Kick,
        Punch
    }
    Actions chosenAction = Actions.None;

    public Actions ChosenAction { get { return chosenAction; } }

    private void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindAction("Punch").performed += Punch;
        playerInput.actions.FindAction("Kick").performed += Kick;
        playerInput.actions.FindAction("Block").performed += Block;

        playerInput.actions.FindAction("WagerUp").performed += WagerUp;
        playerInput.actions.FindAction("WagerDown").performed += WagerDown;
    }

    void Update()
    {

    }
    #region RPS
    void Punch(InputAction.CallbackContext callback)
    {
        chosenAction = Actions.Punch;
        // Testing
        Debug.Log(playerInput.playerIndex.ToString() + " Punch Selected");
    }
    
    void Kick(InputAction.CallbackContext callback)
    {
        chosenAction = Actions.Kick;
        // Testing
        Debug.Log(playerInput.playerIndex.ToString() + " Kick Selected");
    }

    void Block(InputAction.CallbackContext callback)
    {
        chosenAction = Actions.Block;
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

    public void RPSPhase()
    {
        playerInput.actions.FindActionMap("RPS").Enable();
    }
    public void WagerPhase()
    {
        playerInput.actions.FindActionMap("Wager").Enable();
        playerInput.actions.FindActionMap("RPS").Disable();
    }
    public void ActionPhase()
    {
        playerInput.actions.FindActionMap("Wager").Disable();
        playerInput.actions.FindActionMap("RPS").Disable();
    }

    public void ResetChosenAction()
    {
        chosenAction = Actions.None;
    }
}
