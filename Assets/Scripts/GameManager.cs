using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    PlayerController[] players;
    public PlayerController[] Players { get { return players; } }

    [SerializeField] Clock timer;
    [SerializeField] Clock countdown;

    //Testing
    public bool currentPhaseTextShouldAppear = false;
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

    public PlayerController roundWinner;
    public PlayerController roundLoser;

    AnimationHandler[] animationHandlers;

    public enum Phase
    {
        MatchEnd,
        RPS,
        Wager,
        Action
    }
    Phase currentPhase;
    public Phase GetPhase()
    {
        return currentPhase;
    }

    public enum MatchResult
    {
        Win,
        Lose,
        Tie
    }

    void Start()
    {

        EndRound();

        //Testing
        currentPhaseText.text = "";
        roundWinnerText.text = "";
    }

    void Update()
    {
        UpdateText();

        switch (currentPhase)
        {
            ///////////////////////////////////////
            case Phase.RPS:
                if (!countdown.IsZero())
                {
                    break;
                }

                foreach (PlayerController player in players)
                {
                    player.RPSPhase();
                }
                timer.StartClock();

                //Testing
                currentPhaseTextShouldAppear = true;
                roundWinnerTextShouldAppear = false;

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
                for (int i = 0; i < players.Length; i++)
                {
                    animationHandlers[i].SetChoice(players[i].ChosenAction);
                    animationHandlers[i].SetRPSTie(false);
                    if (players[0].ChosenAction == players[1].ChosenAction)
                    {
                        animationHandlers[i].SetRPSTie(true);
                    }
                }

                currentPhase = Phase.Wager;
                timer.RestartClock();

                break;
            ///////////////////////////////////////
            case Phase.Wager:
                foreach (PlayerController player in players)
                {
                    player.WagerPhase();
                }

                if (timer.IsZero())
                {
                    currentPhase = Phase.Action;

                    DetermineRoundWinner();
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (roundWinner == null)
                            animationHandlers[i].SetResult(MatchResult.Tie);
                        else if (roundWinner == players[i])
                            animationHandlers[i].SetResult(MatchResult.Win);
                        else
                            animationHandlers[i].SetResult(MatchResult.Lose);
                    }
                }

                //Testing
                currentPhaseTextShouldAppear = true;

                break;
            ///////////////////////////////////////
            case Phase.Action:
                foreach (PlayerController player in players)
                {
                    player.ActionPhase();
                }

                break;
            ///////////////////////////////////////
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

    public void EndRound()
    {
        //testing
        currentPhaseTextShouldAppear = false;
        roundWinnerTextShouldAppear = true;

        if (HasWonMatch(players[0]) || HasWonMatch(players[1]))
        {
            currentPhase = Phase.MatchEnd;
            return;
        }

        foreach (PlayerController player in players)
        {
            player.playerStamina.resetWager();
            player.ResetChosenAction();
        }

        countdown.RestartClock();
        timer.ResetClock();

        currentPhase = Phase.RPS;

    }

    void DetermineRoundWinner()
    {
        //Determine Round Winner
        int player1Value = (int)players[0].ChosenAction + ((int)players[1].ChosenAction / 3) * 3 * ((int)players[0].ChosenAction % 2);
        int player2Value = (int)players[1].ChosenAction + ((int)players[0].ChosenAction / 3) * 3 * ((int)players[1].ChosenAction % 2);

        if (!timer.IsZero())
        {
            return;
        }
        if (player1Value > player2Value)
        {
            RoundWin(players[0], players[1]);
        }
        else if (player2Value > player1Value)
        {
            RoundWin(players[1], players[0]);
        }
        else if (CompareWager(players[0], players[1]))
        {
            RoundWin(players[0], players[1]);
        }
        else if (CompareWager(players[1], players[0]))
        {
            RoundWin(players[1], players[0]);
        }
        else
        {
            RoundTie();
        }
    }
    void RoundWin(PlayerController winner, PlayerController loser)
    {
        winner.playerStamina.GainStamina(loser.playerStamina.CurrentWager);
        loser.playerStamina.LoseStamina(loser.playerStamina.CurrentWager);
        roundWinner = winner;
        roundLoser = loser;
    }

    void RoundTie()
    {
        roundWinnerText.text = $"Round Winner: Tie!";
        roundWinner = null;
        roundLoser = null;

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

    bool HasWonMatch(PlayerController player)
    {
        return player.playerStamina.CurrentStamina == player.playerStamina.staminaPoints;
    }

    public void EnableGameManager()
    {
        playerSpawner = FindObjectOfType<PlayerSpawner>();

        players = new PlayerController[]
        {
            playerSpawner._players[0].GetComponent<PlayerController>(),
            playerSpawner._players[1].GetComponent<PlayerController>()
        };

        animationHandlers = new AnimationHandler[]
        {
            players[0].GetComponent<AnimationHandler>(),
            players[1].GetComponent<AnimationHandler>()
        };
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
            player1StaminaText.text = $"Player 1 Stamina: {players[0].playerStamina.CurrentStamina}";
            player2StaminaText.text = $"Player 2 Stamina: {players[1].playerStamina.CurrentStamina}";
            player1WagerText.text = $"Player 1 Wager: {players[0].playerStamina.CurrentWager}";
            player2WagerText.text = $"Player 2 Wager: {players[1].playerStamina.CurrentWager}";
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

    public bool CompareWager(PlayerController player1, PlayerController player2)
    {
        return (player1.playerStamina.CurrentWager > player2.playerStamina.CurrentWager);
    }
}