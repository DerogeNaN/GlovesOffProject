using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    PlayerController player1;
    PlayerController player2;

    [SerializeField] Clock timer;
    [SerializeField] Clock countdown;

    //Testing
    bool phaseTextShouldAppear = false;
    public TextMeshProUGUI currentPhaseText;

    bool staminaTextShouldAppear = true;
    public TextMeshProUGUI player1StaminaText;
    public TextMeshProUGUI player2StaminaText;
    public TextMeshProUGUI player1WagerText;
    public TextMeshProUGUI player2WagerText;

    bool roundWinnerTextShouldAppear = false;
    public TextMeshProUGUI roundWinnerText;

    PlayerController roundWinner;

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
        //Testing
        currentPhaseText.text = "";
        roundWinnerText.text = "";
        ResetRound();
    }

    void Update()
    {
        UpdateText();

        switch (currentPhase)
        {
            case Phase.RPS:
                //RPS Logic goes here
                if (countdown.IsZero())
                {
                    player1.RPSPhase();
                    player2.RPSPhase();
                    timer.StartClock();

                    //Testing
                    phaseTextShouldAppear = true;
                    roundWinnerTextShouldAppear = false;
                }

                if (timer.IsZero())
                {
                    if(player1.ChosenAction == PlayerController.Actions.None)
                    {
                        player1.RandomiseAction();
                    }

                    if (player2.ChosenAction == PlayerController.Actions.None)
                    {
                        player2.RandomiseAction();
                    }

                    currentPhase = Phase.Wager;
                    timer.ResetClock();
                    timer.StartClock();
                    Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                }
                break;

            case Phase.Wager:
                //Testing
                phaseTextShouldAppear = true;

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
                //Testing
                phaseTextShouldAppear = true;

                player1.ActionPhase();
                player2.ActionPhase();

                //Determine Round Winner
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
                    if (player1.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints || player2.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints)
                    {
                        timer.ResetClock();
                        timer.StartClock();
                        currentPhase = Phase.MatchEnd;
                    }
                    else
                    {
                        ResetRound();
                        Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                    }

                }
                break;

            case Phase.MatchEnd:
                if (player1.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints)
                {
                    MatchWin(player1);
                }
                if (player2.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints)
                {
                    MatchWin(player2);
                }
                if (timer.IsZero())
                {
                    Application.Quit();
                }
                break;
        }
    }

    void ResetRound()
    {
        player1.playerStamina.resetWager();
        player2.playerStamina.resetWager();
        countdown.ResetClock();
        timer.ResetClock();

        countdown.StartClock();

        currentPhase = Phase.RPS;
        player1.ResetChosenAction();
        player2.ResetChosenAction();

        //testing
        phaseTextShouldAppear = false;
        roundWinnerTextShouldAppear = true;
    }

    void RoundWin(PlayerController winner, PlayerController loser)
    {
        winner.playerStamina.GainStamina(loser.playerStamina.CurrentWager);
        loser.playerStamina.LoseStamina(loser.playerStamina.CurrentWager);
        roundWinner = winner;
        roundWinnerTextShouldAppear = true;
    }

    void RoundTie()
    {
        roundWinnerText.text = $"Round Winner: Tie!";
    }

    void MatchWin(PlayerController winner)
    {
        currentPhase = Phase.MatchEnd;

        //testing
        staminaTextShouldAppear = false;
    }

    public void EnableGameManager()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();

        player1 = playerSpawner._players[0].GetComponent<PlayerController>();
        player2 = playerSpawner._players[1].GetComponent<PlayerController>();
        this.enabled = true;
    }
    
    public void UpdateText()
    {
        if (!staminaTextShouldAppear)
        {
            player1StaminaText.text = "";
            player2StaminaText.text = "";
            player1WagerText.text = "";
            player2WagerText.text = "";
        }
        else
        {
            player1StaminaText.text = $"Player 1 Stamina: {player1.playerStamina.CurrentStamina}";
            player2StaminaText.text = $"Player 2 Stamina: {player2.playerStamina.CurrentStamina}";
            player1WagerText.text = $"Player 1 Wager: {player1.playerStamina.CurrentWager}";
            player2WagerText.text = $"Player 2 Wager: {player2.playerStamina.CurrentWager}";
        }

        if(!roundWinnerTextShouldAppear)
        {
            roundWinnerText.text = "";
        }
        else
        {
            if (roundWinner != null)
            {
                roundWinnerText.text = $"Round Winner: {roundWinner.name}!";
            }
        }

        if (!phaseTextShouldAppear)
        {
            currentPhaseText.text = "";
        }
        else
        {
            currentPhaseText.text = $"Current Phase: {currentPhase}";
        }
    }
}