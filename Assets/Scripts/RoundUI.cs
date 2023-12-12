using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [SerializeField] Sprite dPadLeft;
    [SerializeField] Sprite dPadRight;

    [SerializeField] Sprite aButton;

    [SerializeField] Sprite pkbKeyboard;
    [SerializeField] Sprite pkbController;

    [SerializeField] Sprite wagerUpKeyboard;
    [SerializeField] Sprite wagerDownKeyboard;
    [SerializeField] Sprite wagerUpController;
    [SerializeField] Sprite wagerDownController;

    [SerializeField] Sprite player1MatchWin;
    [SerializeField] Sprite player2MatchWin;


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

    Image player1DPadLeft;
    Image player1DPadRight;
    Image player2DPadLeft;
    Image player2DPadRight;

    GameObject player1JoinButton;
    GameObject player2JoinButton;

    TextMeshProUGUI player1JoinButtonText;
    TextMeshProUGUI player2JoinButtonText;

    Image player1ActionLayout;
    Image player2ActionLayout;

    Image player1WagerUp;
    Image player1WagerDown;
    Image player2WagerUp;
    Image player2WagerDown;

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

        player1DPadLeft = UICanvas.transform.Find("Player1 D-Pad").Find("D-Pad Left").GetComponent<Image>();
        player1DPadRight = UICanvas.transform.Find("Player1 D-Pad").Find("D-Pad Right").GetComponent<Image>();
        player2DPadLeft = UICanvas.transform.Find("Player2 D-Pad").Find("D-Pad Left").GetComponent<Image>();
        player2DPadRight = UICanvas.transform.Find("Player2 D-Pad").Find("D-Pad Right").GetComponent<Image>();

        player1ActionLayout = UICanvas.transform.Find("Player1 Action Layout").GetComponent<Image>();
        player2ActionLayout = UICanvas.transform.Find("Player2 Action Layout").GetComponent<Image>();

        player1WagerUp = UICanvas.transform.Find("Player1 Wager Layout").Find("Wager Up").GetComponent<Image>();
        player1WagerDown = UICanvas.transform.Find("Player1 Wager Layout").Find("Wager Down").GetComponent<Image>();
        player2WagerUp = UICanvas.transform.Find("Player2 Wager Layout").Find("Wager Up").GetComponent<Image>();
        player2WagerDown = UICanvas.transform.Find("Player2 Wager Layout").Find("Wager Down").GetComponent<Image>();

        player1JoinButton = UICanvas.transform.Find("Player1 Join Button").gameObject;
        player2JoinButton = UICanvas.transform.Find("Player2 Join Button").gameObject;

        player1JoinButtonText = player1JoinButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        player2JoinButtonText = player2JoinButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();

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

        player1DPadLeft.enabled = false;
        player1DPadRight.enabled = false;
        player2DPadLeft.enabled = false;
        player2DPadRight.enabled = false;

        player1ActionLayout.enabled = false;
        player2ActionLayout.enabled = false;

        player1WagerUp.enabled = false;
        player1WagerDown.enabled = false;
        player2WagerUp.enabled = false;
        player2WagerDown.enabled = false;
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
                
                RenderActionLayout();
                player1ActionLayout.enabled = true;
                player2ActionLayout.enabled = true;

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

                RenderWagerLayout();
                player1WagerUp.enabled = true;
                player1WagerDown.enabled = true;
                player2WagerUp.enabled = true;
                player2WagerDown.enabled = true;

                player1ActionLayout.enabled = false;
                player2ActionLayout.enabled = false;
                break;
            case GameManager.Phase.Action:
                timer.enabled = false;
                currentPhase.enabled = false;

                player1WagerUp.enabled = false;
                player1WagerDown.enabled = false;
                player2WagerUp.enabled = false;
                player2WagerDown.enabled = false;

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
            matchWinner.sprite = player1MatchWin;
        }
        else if (gameManager.MatchWinner == gameManager.Players[1])
        {
            matchWinner.sprite = player2MatchWin;
        }
    }
    void RenderCurrentPhase(Sprite phase)
    {
        currentPhase.sprite = phase;
    }

    void RenderActionLayout()
    {
        if (gameManager.Players[0].controlSchemeKeyboard)
        {
            player1ActionLayout.sprite = pkbKeyboard;
        }
        else
        {
            player1ActionLayout.sprite = pkbController;
        }
        if (gameManager.Players[1].controlSchemeKeyboard)
        {
            player2ActionLayout.sprite = pkbKeyboard;
        }
        else
        {
            player2ActionLayout.sprite = pkbController;
        }
    }

    void RenderWagerLayout()
    {
        if (gameManager.Players[0].controlSchemeKeyboard)
        {
            player1WagerUp.sprite = wagerUpKeyboard;
            player1WagerDown.sprite = wagerDownKeyboard;
        }
        else
        {
            player1WagerUp.sprite = wagerUpController;
            player1WagerDown.sprite = wagerDownController;
        }
        if (gameManager.Players[1].controlSchemeKeyboard)
        {
            player2WagerUp.sprite = wagerUpKeyboard;
            player2WagerDown.sprite = wagerDownKeyboard;
        }
        else
        {
            player2WagerUp.sprite = wagerUpController;
            player2WagerDown.sprite = wagerDownController;
        }
    }

    public void OnJoin(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1JoinButtonText.text = "READY";
            player1DPadLeft.enabled = true;
            player1DPadRight.enabled = true;
        }
        if (playerNumber == 2)
        {
            player2JoinButtonText.text = "READY";
            player2DPadLeft.enabled = true;
            player2DPadRight.enabled = true;
        }
    }

    public void OnReady(int playerIndex)
    {
        if (playerIndex == 0)
        {
            player1JoinButton.SetActive(false);
            player1DPadLeft.enabled = false;
            player1DPadRight.enabled = false;
        }
        if (playerIndex == 1)
        {
            player2JoinButton.SetActive(false);
            player2DPadLeft.enabled = false;
            player2DPadRight.enabled = false;
        }

    }
}

