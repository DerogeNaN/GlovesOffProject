using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
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

    private VFXManager[] vfxManagers;

    private PauseUI pauseUI;

    [SerializeField] EventSystem eventSystem;

    Singleplayer singleplayer;
    MusicManager musicManager;

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

        vfxManagers = new VFXManager[]
        {
            players[0].GetComponent<VFXManager>(),
            players[1].GetComponent<VFXManager>()
        };

        singleplayer = GameObject.FindGameObjectWithTag("Singleplayer").GetComponent<Singleplayer>();
        musicManager = GameObject.FindGameObjectWithTag("Music Manager").GetComponent<MusicManager>();
    }

    private void Awake()
    {
        pauseUI = GetComponent<PauseUI>();
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
                        eventSystem.SetSelectedGameObject(mainMenuButton.gameObject);
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
        players[0].playerInput.actions.FindActionMap("HatSelection").Disable();
        players[1].playerInput.actions.FindActionMap("HatSelection").Disable();
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
        vfxManagers[0].ClearVFX();
        vfxManagers[1].ClearVFX();
    }
    #endregion

    public void PauseGame(InputAction.CallbackContext callback, PlayerController p)
    {
        if (players == null) return;

        paused = true;
        Time.timeScale = 0;

        pauseUI.OnGamePause();
        for (int i = 0; i < Players.Length; i++)
        {
            if (p == Players[i])
            {
                Players[i].playerInput.actions.FindActionMap("HatSelection").Disable();
                Players[i].playerInput.actions.FindActionMap("UIPause").Enable();
                Players[i].playerInput.actions.FindActionMap("UI").Enable();
                eventSystem.GetComponent<InputSystemUIInputModule>().actionsAsset = p.playerInput.actions;
            }
            else if (p != Players[i])
            {
                Players[i].DisablePreviousActionMap();
                Players[i].playerInput.actions.FindActionMap("InGame").Disable();
                Players[i].playerInput.actions.FindActionMap("UIPause").Disable();
                Players[i].playerInput.actions.FindActionMap("UI").Disable();
                Players[i].playerInput.actions.FindActionMap("HatSelection").Disable();
            }
        }
    }
    public void ResumeGame(InputAction.CallbackContext callback, PlayerController p)
    {
        paused = false;
        Time.timeScale = 1;

        pauseUI.OnGamePause();
        for (int i = 0; i < Players.Length; i++)
        {
            if (p == Players[i])
            {
                Players[i].playerInput.actions.FindActionMap("UIPause").Disable();
                Players[i].playerInput.actions.FindActionMap("UI").Disable();
            }
            else if (p != Players[i])
            {
                Players[i].EnablePreviousActionMap();
                Players[i].playerInput.actions.FindActionMap("InGame").Enable();
            }
        }

    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Assumed");
        paused = false;
        Time.timeScale = 1;
        players[0].playerInput.actions.Disable();
        players[1].playerInput.actions.Disable();
        singleplayer.singleplayer = false;
        Destroy(singleplayer.gameObject);
        Destroy(musicManager.gameObject);
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}