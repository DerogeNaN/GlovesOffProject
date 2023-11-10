using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private GameManager gameManager;
    private PlayerController playerController;
    private PlayerController.Actions actionChoice = PlayerController.Actions.None;


    private GameManager.Phase oldPhase;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerController = GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetPhase() != oldPhase)
        {
            oldPhase = gameManager.GetPhase();
            if (oldPhase == GameManager.Phase.Action)
            {
                animator.SetBool("Action Phase Start", true);
            }
            if (oldPhase != GameManager.Phase.Action)
            {
                animator.SetBool("Action Phase Start", false);
            }
        }



    }

    void RPSAnimationTrigger(PlayerController player, PlayerController.Actions actionSelected, string animationState)
    {
        if (player.ChosenAction == actionSelected/* && Player loses round*/)
        {
            animator.SetBool(animationState, true);
            if (oldPhase != GameManager.Phase.Action)
            {
                animator.SetBool(animationState, false);
            }
        }
    }

    public void ResetActionChoices()
    {
        actionChoice = PlayerController.Actions.None;

    }

    public void SetChoice(PlayerController.Actions choice)
    {
        actionChoice = choice;
    }  

    public void SetResult(GameManager.MatchResult matchResult)
    {
        StringBuilder buildMeAString = new StringBuilder(16);

        switch (actionChoice)
        {
            case PlayerController.Actions.Block:
                buildMeAString.Append("Block");
                break;

            case PlayerController.Actions.Kick:
                buildMeAString.Append("Kick");
                break;

            case PlayerController.Actions.Punch:
                buildMeAString.Append("Punch");
                break;

            default:

                break;
        }
        switch (matchResult)
        {
            case GameManager.MatchResult.Win:
                buildMeAString.Append("Win");
                break;

            case GameManager.MatchResult.Lose:
                buildMeAString.Append("Lose");
                break;

            case GameManager.MatchResult.Tie:
                buildMeAString.Append("Tie");
                break;

            default:
                break;
        }

        animator.SetTrigger(buildMeAString.ToString());
    }
}
