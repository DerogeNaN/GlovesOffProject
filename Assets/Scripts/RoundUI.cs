using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundUI : MonoBehaviour
{
    GameManager gameManager;
    Canvas UICanvas;

    TextMeshProUGUI currentPhase;

    TextMeshProUGUI roundWinner;

    TextMeshProUGUI matchWinner;

    TextMeshProUGUI p1RPSConfirm;
    TextMeshProUGUI p2RPSConfirm;

    [SerializeField] Sprite blankStamina;
    [SerializeField] Sprite blueStamina;
    [SerializeField] Sprite orangeStamina;

    [SerializeField] Sprite blankWager;
    [SerializeField] Sprite blueWager;
    [SerializeField] Sprite yellowWager;

    Image[] P1Stamina = new Image[8];
    Image[] P2Stamina = new Image[8];

    Image[] P1Wager = new Image[3];
    Image[] P2Wager = new Image[3];

    float p1ActionChosenTime = 0.3f;
    float p1CurrentTime = 0;

    float p2ActionChosenTime = 0.3f;
    float p2CurrentTime = 0;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        UICanvas = GameObject.FindObjectOfType<Canvas>();
        currentPhase = UICanvas.transform.Find("Current Phase").GetComponent<TextMeshProUGUI>();

        roundWinner = UICanvas.transform.Find("Round Winner").GetComponent<TextMeshProUGUI>();
        matchWinner = UICanvas.transform.Find("Match Winner").GetComponent<TextMeshProUGUI>();

        p1RPSConfirm = UICanvas.transform.Find("P1 RPS Confirm").GetComponent<TextMeshProUGUI>();
        p2RPSConfirm = UICanvas.transform.Find("P2 RPS Confirm").GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < P1Stamina.Length; i++)
        {
            P1Stamina[i] = UICanvas.transform.Find("Player1 Stamina").GetChild(i).GetComponent<Image>();
        }
        for (int i = 0; i < P2Stamina.Length; i++)
        {
            P2Stamina[i] = UICanvas.transform.Find("Player2 Stamina").GetChild(i).GetComponent<Image>();
        }

        for (int i = 0; i < P1Wager.Length; i++)
        {
            P1Wager[i] = UICanvas.transform.Find("Player1 Wager").GetChild(i).GetComponent<Image>();
        }
        for (int i = 0; i < P2Wager.Length; i++)
        {
            P2Wager[i] = UICanvas.transform.Find("Player2 Wager").GetChild(i).GetComponent<Image>();
        }
    }

    void Start()
    {
        currentPhase.enabled = false;

        p1RPSConfirm.enabled = false;
        p2RPSConfirm.enabled = false;
        foreach (Image stamina in P1Stamina)
        {
            stamina.enabled = false;
        }
        foreach (Image stamina in P2Stamina)
        {
            stamina.enabled = false;
        }

        foreach (Image wager in P1Wager)
        {
            wager.enabled = false;
        }
        foreach (Image wager in P2Wager)
        {
            wager.enabled = false;
        }

        roundWinner.enabled = false;
        matchWinner.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameManager.isActiveAndEnabled)
        {
            return;
        }
        UpdateText();
        switch (gameManager.CurrentPhase)
        {
            case GameManager.Phase.RoundStart:
                currentPhase.enabled = false;
                RenderStamina(P1Stamina, gameManager.Players[0], blueStamina);
                RenderStamina(P2Stamina, gameManager.Players[1], orangeStamina);

                foreach (Image stamina in P1Stamina)
                {
                    stamina.enabled = false;
                }
                foreach (Image stamina in P2Stamina)
                {
                    stamina.enabled = false;
                }

                foreach (Image wager in P1Wager)
                {
                    wager.enabled = false;
                }
                foreach (Image wager in P2Wager)
                {
                    wager.enabled = false;
                }

                roundWinner.enabled = false;
                matchWinner.enabled = false;
                break;

            case GameManager.Phase.RPS:
                currentPhase.enabled = true;
                p1CurrentTime = RenderActionChosen(p1RPSConfirm, gameManager.Players[0], p1ActionChosenTime, p1CurrentTime);
                p2CurrentTime = RenderActionChosen(p2RPSConfirm, gameManager.Players[1], p2ActionChosenTime, p2CurrentTime);

                foreach (Image stamina in P1Stamina)
                {
                    stamina.enabled = true;
                }
                foreach (Image stamina in P2Stamina)
                {
                    stamina.enabled = true;
                }

                roundWinner.enabled = false;
                break;
            case GameManager.Phase.Wager:
                p1RPSConfirm.enabled = false;
                p2RPSConfirm.enabled = false;

                foreach (Image wager in P1Wager)
                {
                    wager.enabled = true;
                }
                foreach (Image wager in P2Wager)
                {
                    wager.enabled = true;
                }
                RenderWager(P1Wager, gameManager.Players[0], blueWager);
                RenderWager(P2Wager, gameManager.Players[1], yellowWager);
                break;
            case GameManager.Phase.Action:
                currentPhase.enabled = false;

                foreach (Image wager in P1Wager)
                {
                    wager.enabled = false;
                }
                foreach (Image wager in P2Wager)
                {
                    wager.enabled = false;
                }
                break;
            case GameManager.Phase.RoundEnd:
                RenderStamina(P1Stamina, gameManager.Players[0], blueStamina);
                RenderStamina(P2Stamina, gameManager.Players[1], orangeStamina);
                roundWinner.enabled = true;
                break;
            case GameManager.Phase.MatchEnd:
                foreach (Image stamina in P1Stamina)
                {
                    stamina.enabled = false;
                }
                foreach (Image stamina in P2Stamina)
                {
                    stamina.enabled = false;
                }

                currentPhase.enabled = false;
                roundWinner.enabled = false;

                matchWinner.enabled = true;
                break;
            default:
                break;
        }
    }

    void UpdateText()
    {
        currentPhase.text = $"Current Phase: {gameManager.CurrentPhase}";

        if (gameManager.MatchWinner != null)
        {
            matchWinner.text = $"{gameManager.MatchWinner.name} wins!";
        }

        if (gameManager.RoundWinner == null)
        {
            roundWinner.text = $"Tie!";
        }
        else
        {
            roundWinner.text = $"Round Winner: {gameManager.RoundWinner.name}!";
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
                playerStamina[i].sprite = blankStamina;
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
                playerWager[i].sprite = blankWager;
            }
        }
    }

    float RenderActionChosen(TextMeshProUGUI actionChosen, PlayerController player, float actionChosenTime, float currentTime)
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
}

