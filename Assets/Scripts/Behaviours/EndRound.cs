using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRound : StateMachineBehaviour
{
    GameManager gameManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameManager.GoToRoundEnd();
    }
}
