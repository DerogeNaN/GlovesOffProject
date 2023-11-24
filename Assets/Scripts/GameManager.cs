using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerSpawner playerSpawner;
    PlayerController[] players;
    public PlayerController[] Players { get { return players; } }

    public Clock timer;
    public Clock countdown;
    public Clock roundBuffer;

    ScreenFade screenFade;

    PlayerController roundWinner;
    public PlayerController RoundWinner { get { return roundWinner; } }

    PlayerController matchWinner;
    public PlayerController MatchWinner { get { return matchWinner; } }

    AnimationHandler[] animationHandlers;

    [SerializeField] Button mainMenuButton;

    bool paused = false;
    public bool Paused { get { return paused; } }

    public enum Phase
    {
        RoundBuffer,
        RoundStart,
        RPS,
        Wager,
        Action,
        RoundEnd,
        MatchEnd
    }
    Phase currentPhase;
    public Phase CurrentPhase
    {
        get { return currentPhase; }
    }

    public enum MatchResult
    {
        Win,
        Lose,
        Tie
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
        foreach (AnimationHandler handle in animationHandlers)
        {
            handle.EnableAnimationHandler();
        }
        this.enabled = true;

        screenFade = GetComponent<ScreenFade>();
    }

    void Start()
    {
        GoToRoundStart();
    }

    void Update()
    {
        switch (currentPhase)
        {
            case Phase.RoundStart:
                if (!countdown.IsZero())
                {
                    break;
                }
                GoToRPS();
                break;
            ///////////////////////////////////////
            case Phase.RPS:
                if (!timer.IsZero())
                {
                    break;
                }

                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].ChosenAction == PlayerController.Actions.None)
                        players[i].RandomiseAction();
                    animationHandlers[i].SetChoice(players[i].ChosenAction);
                }

                for (int i = 0; i < players.Length; i++)
                {
                    if (players[0].ChosenAction == players[1].ChosenAction)
                    {
                        animationHandlers[i].SetRPSTie(true);
                    }
                    else
                    {
                        animationHandlers[i].SetRPSTie(false);
                    }
                }

                GoToWager();
                break;
            ///////////////////////////////////////
            case Phase.Wager:
                if (!timer.IsZero())
                {
                    break;
                }
                GoToAction();
                break;
            ///////////////////////////////////////
            case Phase.Action:
                break;
            ///////////////////////////////////////
            case Phase.RoundEnd:
                if (screenFade.IsFading)
                {
                    return;
                }
                GoToRoundBuffer();
                break;
            ///////////////////////////////////////
            case Phase.MatchEnd:
                foreach (PlayerController player in players)
                {
                    if (player.playerStamina.CurrentStamina == player.playerStamina.staminaPoints)
                    {
                        MatchWin(player);
                        mainMenuButton.gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            case Phase.RoundBuffer:
                if (roundBuffer.IsZero())
                {
                    EndRound();
                }
                break;
        }
    }

    void EndRound()
    {
        screenFade.FadeIntoScene();
        if (HasWonMatch(players[0]) || HasWonMatch(players[1]))
        {
            GoToMatchEnd();
            return;
        }

        foreach (PlayerController player in players)
        {
            player.playerStamina.resetWager();
            player.ResetChosenAction();
        }

        GoToRoundStart();
    }

    void DetermineRoundWinner()
    {
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
    }

    void RoundTie()
    {
        roundWinner = null;
    }

    void MatchWin(PlayerController winner)
    {
        matchWinner = winner;
    }

    bool HasWonMatch(PlayerController player)
    {
        return player.playerStamina.CurrentStamina == player.playerStamina.staminaPoints;
    }

    public bool CompareWager(PlayerController player1, PlayerController player2)
    {
        return (player1.playerStamina.CurrentWager > player2.playerStamina.CurrentWager);
    }

    #region Go to phase
    void GoToRoundStart()
    {
        countdown.RestartClock();
        currentPhase = Phase.RoundStart;
    }
    void GoToRPS()
    {
        foreach (PlayerController player in players)
        {
            player.RPSPhase();
        }
        timer.RestartClock();
        currentPhase = Phase.RPS;
    }
    void GoToWager()
    {
        foreach (PlayerController player in players)
        {
            player.WagerPhase();
        }
        timer.RestartClock();
        currentPhase = Phase.Wager;
    }
    void GoToAction()
    {
        DetermineRoundWinner();
        for (int i = 0; i < players.Length; i++)
        {
            if (roundWinner == null)
            {
                animationHandlers[i].SetResult(MatchResult.Tie);
            }
            else if (roundWinner == players[i])
            {
                animationHandlers[i].SetResult(MatchResult.Win);
            }
            else
            {
                animationHandlers[i].SetResult(MatchResult.Lose);
            }
            players[i].playerStamina.PreviousWager = players[i].playerStamina.CurrentWager;
        }
        foreach (PlayerController player in players)
        {
            player.ActionPhase();
        }
        currentPhase = Phase.Action;
    }
    public void GoToRoundEnd()
    {
        screenFade.FadeOutOfScene();
        currentPhase = Phase.RoundEnd;
    }
    void GoToMatchEnd()
    {
        currentPhase = Phase.MatchEnd;
    }
    void GoToRoundBuffer()
    {
        roundBuffer.RestartClock();
        currentPhase = Phase.RoundBuffer;
    }
    #endregion

    public void PauseGame(InputAction.CallbackContext callback, PlayerController p)
    {
        paused = true;
        Time.timeScale = 0;
        Debug.Log("The game is paused");
        for (int i = 0; i < players.Length; i++)
        {
            if (p == players[i])
            {
                players[i].playerInput.actions.FindActionMap("UI").Enable();
            }
            else if (p != players[i])
            {
                players[i].DisablePreviousActionMap();
                players[i].playerInput.actions.FindActionMap("InGame").Disable();
            }
        }
    }
    public void ResumeGame(InputAction.CallbackContext callback, PlayerController p)
    {
        paused = false;
        Time.timeScale = 1;
        Debug.Log("The game will resume");
        for (int i = 0; i < players.Length; i++)
        {
            if (p == players[i])
            {
                players[i].playerInput.actions.FindActionMap("UI").Disable();
            }
            else if (p != players[i])
            {
                players[i].EnablePreviousActionMap();
                players[i].playerInput.actions.FindActionMap("InGame").Enable();
            }
        }

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}