using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    PlayerController[] players;
    PlayerController player1;
    PlayerController player2;

    [SerializeField] Clock timer;
    [SerializeField] Clock countdown;

    //Testing
    bool currentPhaseTextShouldAppear = false;
    public TextMeshProUGUI currentPhaseText;

    bool staminaTextShouldAppear = true;
    public TextMeshProUGUI player1StaminaText;
    public TextMeshProUGUI player2StaminaText;
    public TextMeshProUGUI player1WagerText;
    public TextMeshProUGUI player2WagerText;

    bool roundWinnerTextShouldAppear = false;
    public TextMeshProUGUI roundWinnerText;

    bool matchWinnerTextShouldAppear = false;
    public TextMeshProUGUI matchWinnerText;

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
        players = new PlayerController[] { player1, player2 };
        ResetRound();

        //Testing
        currentPhaseText.text = "";
        roundWinnerText.text = "";
    }

    void Update()
    {
        UpdateText();

        switch (currentPhase)
        {
            case Phase.RPS:
                if (countdown.IsZero())
                {
                    foreach (PlayerController player in players)
                    {
                        player.RPSPhase();
                    }
                    timer.StartClock();

                    //Testing
                    currentPhaseTextShouldAppear = true;
                    roundWinnerTextShouldAppear = false;
                }

                if (!timer.IsZero())
                {
                    break;
                }
                foreach (PlayerController player in players)
                {
                    if (player.ChosenAction == PlayerController.Actions.None)
                    {
                        player.RandomiseAction();
                    }
                }

                currentPhase = Phase.Wager;
                timer.RestartClock();

                //Testing
                Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                break;

            case Phase.Wager:

                foreach (PlayerController player in players)
                {
                    player.WagerPhase();
                }

                //Testing
                currentPhaseTextShouldAppear = true;

                if (timer.IsZero())
                {
                    currentPhase = Phase.Action;
                    timer.RestartClock();

                    //Testing
                    Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
                }
                break;

            case Phase.Action:

                foreach (PlayerController player in players)
                {
                    player.ActionPhase();
                }

                //Testing
                currentPhaseTextShouldAppear = true;

                RoundOver();
                break;

            case Phase.MatchEnd:
                foreach (PlayerController player in players)
                {
                    if (player.playerStamina.CurrentStamina == player.playerStamina.staminaPoints)
                    {
                        MatchWin(player);
                        break;
                    }
                }
                break;
        }
    }

    void ResetRound()
    {
        foreach (PlayerController player in players)
        {
            player.playerStamina.resetWager();
            player.ResetChosenAction();
        }

        countdown.RestartClock();
        timer.ResetClock();

        currentPhase = Phase.RPS;

        //testing
        currentPhaseTextShouldAppear = false;
        roundWinnerTextShouldAppear = true;
    }

    void RoundOver()
    {
        //Determine Round Winner
        int player1Value = (int)player1.ChosenAction + ((int)player2.ChosenAction / 3) * 3 * ((int)player1.ChosenAction % 2);
        int player2Value = (int)player2.ChosenAction + ((int)player1.ChosenAction / 3) * 3 * ((int)player2.ChosenAction % 2);

        if (!timer.IsZero())
        {
            return;
        }
        if (player1Value > player2Value)
        {
            RoundWin(player1, player2);
        }
        else if (player2Value > player1Value)
        {
            RoundWin(player2, player1);
        }
        else if (player1.playerStamina.CurrentWager > player2.playerStamina.CurrentWager)
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


        if (player1.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints || player2.playerStamina.CurrentStamina == player1.playerStamina.staminaPoints)
        {
            currentPhase = Phase.MatchEnd;
        }
        else
        {
            ResetRound();
            Debug.Log($"Phase Finished. Current Phase: {currentPhase}");
        }
    }
    void RoundWin(PlayerController winner, PlayerController loser)
    {
        winner.playerStamina.GainStamina(loser.playerStamina.CurrentWager);
        loser.playerStamina.LoseStamina(loser.playerStamina.CurrentWager);
        roundWinner = winner;

        //Testing
        roundWinnerTextShouldAppear = true;
    }

    void RoundTie()
    {
        roundWinnerText.text = $"Round Winner: Tie!";
    }

    void MatchWin(PlayerController winner)
    {
        //todo: Declare the match winner
        //todo: Go to main menu after a trigger

        //testing
        staminaTextShouldAppear = false;
        matchWinnerText.text = $"{winner.name} wins!";
        currentPhaseTextShouldAppear = false;
        roundWinnerTextShouldAppear = false;
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

        if (!roundWinnerTextShouldAppear)
        {
            roundWinnerText.text = "";
        }
        else if (roundWinner != null)
        {
            roundWinnerText.text = $"Round Winner: {roundWinner.name}!";
        }

        if (!currentPhaseTextShouldAppear)
        {
            currentPhaseText.text = "";
        }
        else
        {
            currentPhaseText.text = $"Current Phase: {currentPhase}";
        }

        if (!matchWinnerTextShouldAppear)
        {
            matchWinnerText.text = "";
        }
    }
}