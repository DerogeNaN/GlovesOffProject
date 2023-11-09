using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    PlayerController player1;
    PlayerController player2;

    [SerializeField] Clock timer;
    [SerializeField] Clock countdown;
    enum Phase
    {
        MatchEnd,
        RPS,
        Wager,
        Action
    }
    Phase currentPhase;

    void Start()
    {
        ResetRound();
    }

    void Update()
    {
        switch (currentPhase)
        {
            case Phase.RPS:
                //RPS Logic goes here
                if (countdown.IsZero())
                {
                    player1.RPSPhase();
                    player2.RPSPhase();
                    timer.StartClock();
                }

                if (timer.IsZero())
                {
                    currentPhase = Phase.Wager;
                    timer.ResetClock();
                    timer.StartClock();
                    Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                }
                break;

            case Phase.Wager:
                //Disable RPS Action Map
                //Enable Wager Action Map
                player1.WagerPhase();
                player2.WagerPhase();
                //Wager Logic goes here

                if (timer.IsZero())
                {
                    currentPhase = Phase.Action;
                    timer.ResetClock();
                    timer.StartClock();
                    Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                }
                break;

            case Phase.Action:
                //Disable Wager Action Map
                player1.ActionPhase();
                player2.ActionPhase();
                //Action Logic goes here
                int player1Value = (int)player1.ChosenAction + ((int)player2.ChosenAction / 3) * 3 * ((int)player1.ChosenAction % 2);
                int player2Value = (int)player2.ChosenAction + ((int)player1.ChosenAction / 3) * 3 * ((int)player2.ChosenAction % 2);

                if (timer.IsZero())
                {
                    if (player1Value > player2Value)
                    {
                        RoundWin(player1, player2);
                    }
                    else if (player2Value > player1Value)
                    {
                        RoundWin(player2, player1);
                    }
                    else
                    {
                        if (player1.playerStamina.CurrentWager > player2.playerStamina.CurrentWager)
                        {
                            RoundWin(player1, player2);
                        }
                        else if (player2.playerStamina.CurrentWager > player1.playerStamina.CurrentWager)
                        {
                            RoundWin(player2, player1);
                        }
                        else
                        {
                            RoundTie();
                        }

                    }
                    if (player1.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints)
                    {
                        MatchWin(player1);
                    }
                    if (player2.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints)
                    {
                        MatchWin(player2);
                    }
                    else
                    {
                        ResetRound();
                        Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                    }
                }
                break;
        }
    }

    void ResetRound()
    {
        countdown.ResetClock();
        timer.ResetClock();

        countdown.StartClock();

        currentPhase = Phase.RPS;
        player1.ResetChosenAction();
        player2.ResetChosenAction();
    }

    void RoundWin(PlayerController winner, PlayerController loser)
    {
        winner.playerStamina.GainStamina(loser.playerStamina.CurrentWager);
        loser.playerStamina.LoseStamina(loser.playerStamina.CurrentWager);
        Debug.Log($"Winner: {winner}");
    }

    void RoundLose(PlayerController player)
    {

    }

    void RoundTie()
    {
        Debug.Log($"Tie");
    }

    void MatchWin(PlayerController winner)
    {
        currentPhase = Phase.MatchEnd;
    }

    public void EnableGameManager()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();

        player1 = playerSpawner._players[0].GetComponent<PlayerController>();
        player2 = playerSpawner._players[1].GetComponent<PlayerController>();
        this.enabled = true;
    }
}