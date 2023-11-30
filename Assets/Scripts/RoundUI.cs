using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    GameManager gameManager;
    Canvas UICanvas;

    Image currentPhase;

    Image p1RPSConfirm;
    Image p2RPSConfirm;

    [SerializeField] Sprite blankStaminaSprite;
    [SerializeField] Sprite p1StaminaSprite;
    [SerializeField] Sprite p2StaminaSprite;

    [SerializeField] Sprite blankWagerSprite;
    [SerializeField] Sprite p1WagerSprite;
    [SerializeField] Sprite p2WagerSprite;

    [SerializeField] Sprite clock1Sprite;
    [SerializeField] Sprite clock2Sprite;
    [SerializeField] Sprite clock3Sprite;

    [SerializeField] Sprite roundTieSprite;
    [SerializeField] Sprite player1WinsSprite;
    [SerializeField] Sprite player2WinsSprite;

    [SerializeField] Sprite rpsSprite;
    [SerializeField] Sprite wagerSprite;


    Image p1StaminaBar;
    Image p2StaminaBar;
    Image[] p1Stamina = new Image[8];
    Image[] p2Stamina = new Image[8];

    Image[] p1Wager = new Image[3];
    Image[] p2Wager = new Image[3];

    Image timer;
    Image countdown;

    Image roundWinner;
    Image matchWinner;

    float p1ActionChosenTime = 0.3f;
    float p1CurrentTime = 0;

    float p2ActionChosenTime = 0.3f;
    float p2CurrentTime = 0;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        UICanvas = GameObject.FindObjectOfType<Canvas>();
        currentPhase = UICanvas.transform.Find("Current Phase").GetComponent<Image>();

        roundWinner = UICanvas.transform.Find("Round Winner").GetComponent<Image>();
        matchWinner = UICanvas.transform.Find("Match Winner").GetComponent<Image>();

        p1RPSConfirm = UICanvas.transform.Find("RPS Confirm").Find("P1 RPS Confirm").GetComponent<Image>();
        p2RPSConfirm = UICanvas.transform.Find("RPS Confirm").Find("P2 RPS Confirm").GetComponent<Image>();

        p1StaminaBar = UICanvas.transform.Find("Player1 Stamina").Find("Player 1 Stamina Bar").GetComponent<Image>();
        p2StaminaBar = UICanvas.transform.Find("Player2 Stamina").Find("Player 2 Stamina Bar").GetComponent<Image>();
        for (int i = 0; i < p1Stamina.Length; i++)
        {
            p1Stamina[i] = UICanvas.transform.Find("Player1 Stamina").GetChild(i).GetComponent<Image>();
        }
        for (int i = 0; i < p2Stamina.Length; i++)
        {
            p2Stamina[i] = UICanvas.transform.Find("Player2 Stamina").GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < p1Wager.Length; i++)
        {
            p1Wager[i] = UICanvas.transform.Find("Player1 Wager").GetChild(i).GetComponent<Image>();
        }
        for (int i = 0; i < p2Wager.Length; i++)
        {
            p2Wager[i] = UICanvas.transform.Find("Player2 Wager").GetChild(i).GetComponent<Image>();
        }

        timer = UICanvas.transform.Find("Clocks").Find("Timer").GetComponent<Image>();
        countdown = UICanvas.transform.Find("Clocks").Find("Countdown").GetComponent<Image>();
    }

    void Start()
    {
        currentPhase.enabled = false;

        p1RPSConfirm.enabled = false;
        p2RPSConfirm.enabled = false;

        p1StaminaBar.enabled = false;
        p2StaminaBar.enabled = false;

        foreach (Image stamina in p1Stamina)
        {
            stamina.enabled = false;
        }
        foreach (Image stamina in p2Stamina)
        {
            stamina.enabled = false;
        }

        foreach (Image wager in p1Wager)
        {
            wager.enabled = false;
        }
        foreach (Image wager in p2Wager)
        {
            wager.enabled = false;
        }

        roundWinner.enabled = false;
        matchWinner.enabled = false;

        countdown.enabled = false;
        timer.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameManager.isActiveAndEnabled)
        {
            return;
        }
        switch (gameManager.CurrentPhase)
        {
            case GameManager.Phase.RoundStart:
                countdown.enabled = true;
                currentPhase.enabled = false;

                RenderStamina(p1Stamina, gameManager.Players[0], p1StaminaSprite);
                RenderStamina(p2Stamina, gameManager.Players[1], p2StaminaSprite);
                RenderClock(countdown, gameManager.countdown);

                p1StaminaBar.enabled = false;
                p2StaminaBar.enabled = false;

                foreach (Image stamina in p1Stamina)
                {
                    stamina.enabled = false;
                }
                foreach (Image stamina in p2Stamina)
                {
                    stamina.enabled = false;
                }

                foreach (Image wager in p1Wager)
                {
                    wager.enabled = false;
                }
                foreach (Image wager in p2Wager)
                {
                    wager.enabled = false;
                }

                roundWinner.enabled = false;
                matchWinner.enabled = false;
                break;

            case GameManager.Phase.RPS:
                countdown.enabled = false;
                timer.enabled = true;
                currentPhase.enabled = true;

                p1CurrentTime = RenderActionChosen(p1RPSConfirm, gameManager.Players[0], p1ActionChosenTime, p1CurrentTime);
                p2CurrentTime = RenderActionChosen(p2RPSConfirm, gameManager.Players[1], p2ActionChosenTime, p2CurrentTime);
                RenderClock(timer, gameManager.timer);
                RenderCurrentPhase(rpsSprite);

                p1StaminaBar.enabled = true;
                p2StaminaBar.enabled = true;

                foreach (Image stamina in p1Stamina)
                {
                    stamina.enabled = true;
                }
                foreach (Image stamina in p2Stamina)
                {
                    stamina.enabled = true;
                }

                roundWinner.enabled = false;
                break;
            case GameManager.Phase.Wager:
                timer.enabled = true;
                p1RPSConfirm.enabled = false;
                p2RPSConfirm.enabled = false;

                foreach (Image wager in p1Wager)
                {
                    wager.enabled = true;
                }
                foreach (Image wager in p2Wager)
                {
                    wager.enabled = true;
                }
                RenderCurrentPhase(wagerSprite);
                RenderClock(timer, gameManager.timer);
                RenderWager(p1Wager, gameManager.Players[0], p1WagerSprite);
                RenderWager(p2Wager, gameManager.Players[1], p2WagerSprite);
                break;
            case GameManager.Phase.Action:
                timer.enabled = false;
                currentPhase.enabled = false;

                foreach (Image wager in p1Wager)
                {
                    wager.enabled = false;
                }
                foreach (Image wager in p2Wager)
                {
                    wager.enabled = false;
                }
                break;
            case GameManager.Phase.RoundEnd:
                RenderStamina(p1Stamina, gameManager.Players[0], p1StaminaSprite);
                RenderStamina(p2Stamina, gameManager.Players[1], p2StaminaSprite);
                RenderRoundWinner();
                roundWinner.enabled = true;
                break;
            case GameManager.Phase.MatchEnd:

                p1StaminaBar.enabled = false;
                p2StaminaBar.enabled = false;

                foreach (Image stamina in p1Stamina)
                {
                    stamina.enabled = false;
                }
                foreach (Image stamina in p2Stamina)
                {
                    stamina.enabled = false;
                }

                RenderMatchWinner();
                currentPhase.enabled = false;
                roundWinner.enabled = false;

                matchWinner.enabled = true;
                break;
            case GameManager.Phase.RoundBuffer:
                roundWinner.enabled = false;
                break;
            default:
                break;
        }
    }

    void RenderStamina(Image[] playerStamina, PlayerController player, Sprite active)
    {
        for (int i = 0; i < playerStamina.Length; i++)
        {
            if (player.playerStamina.CurrentStamina > i)
            {
                playerStamina[i].sprite = active;
            }
            else
            {
                playerStamina[i].sprite = blankStaminaSprite;
            }
        }
    }
    void RenderWager(Image[] playerWager, PlayerController player, Sprite active)
    {
        for (int i = 0; i < playerWager.Length; i++)
        {
            if (player.playerStamina.CurrentWager > i)
            {
                playerWager[i].sprite = active;
            }
            else
            {
                playerWager[i].sprite = blankWagerSprite;
            }
        }
    }

    float RenderActionChosen(Image actionChosen, PlayerController player, float actionChosenTime, float currentTime)
    {
        if (player.newActionChosen)
        {
            currentTime = actionChosenTime;
            player.newActionChosen = false;
        }
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            actionChosen.enabled = true;
            return currentTime;
        }
        actionChosen.enabled = false;
        return 0;
    }

    void RenderClock(Image clockImage, Clock clock)
    {
        if (clock.IsZero())
        {
            clockImage.enabled = false;
        }
        if (clock.CurrentTime <= 3 && clock.CurrentTime > 2)
        {
            clockImage.sprite = clock3Sprite;
        }
        else if (clock.CurrentTime <= 2 && clock.CurrentTime > 1)
        {
            clockImage.sprite = clock2Sprite;
        }
        else if (clock.CurrentTime <= 1 && clock.CurrentTime > 0)
        {
            clockImage.sprite = clock1Sprite;
        }
    }
    
    void RenderRoundWinner()
    {
        if (gameManager.RoundWinner == null)
        {
            roundWinner.sprite = roundTieSprite;
        }
        else if (gameManager.RoundWinner == gameManager.Players[0])
        {
            roundWinner.sprite = player1WinsSprite;
        }
        else if (gameManager.RoundWinner == gameManager.Players[1])
        {
            roundWinner.sprite = player2WinsSprite;
        }
    }
    void RenderMatchWinner()
    {
        if (gameManager.MatchWinner == gameManager.Players[0])
        {
            matchWinner.sprite = player1WinsSprite;
        }
        else if (gameManager.MatchWinner == gameManager.Players[1])
        {
            matchWinner.sprite = player2WinsSprite;
        }
    }
    void RenderCurrentPhase(Sprite phase)
    {
        currentPhase.sprite = phase;
    }
}

