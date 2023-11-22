using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Canvas UICanvas;

    TextMeshProUGUI currentPhase;

    TextMeshProUGUI player1Stamina;
    TextMeshProUGUI player2Stamina;

    TextMeshProUGUI player1Wager;
    TextMeshProUGUI player2Wager;

    TextMeshProUGUI roundWinner;

    TextMeshProUGUI matchWinner;

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        currentPhase = UICanvas.transform.Find("Current Phase").GetComponent<TextMeshProUGUI>();

        player1Stamina = UICanvas.transform.Find("Player 1 Stamina").GetComponent<TextMeshProUGUI>();
        player2Stamina = UICanvas.transform.Find("Player 2 Stamina").GetComponent<TextMeshProUGUI>();

        player1Wager = UICanvas.transform.Find("Player 1 Wager").GetComponent<TextMeshProUGUI>();
        player2Wager = UICanvas.transform.Find("Player 2 Wager").GetComponent<TextMeshProUGUI>();

        roundWinner = UICanvas.transform.Find("Round Winner").GetComponent<TextMeshProUGUI>();
        matchWinner = UICanvas.transform.Find("Match Winner").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        currentPhase.enabled = false;

        player1Stamina.enabled = false;
        player2Stamina.enabled = false;

        player1Wager.enabled = false;
        player2Wager.enabled = false;

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

                player1Stamina.enabled = false;
                player2Stamina.enabled = false;

                player1Wager.enabled = false;
                player2Wager.enabled = false;

                roundWinner.enabled = false;
                matchWinner.enabled = false;
                break;

            case GameManager.Phase.RPS:
                roundWinner.enabled = false;

                player1Stamina.enabled = true;
                player2Stamina.enabled = true;

                currentPhase.enabled = true;
                break;
            case GameManager.Phase.Wager:
                player1Wager.enabled = true;
                player2Wager.enabled = true;
                break;
            case GameManager.Phase.Action:
                currentPhase.enabled = false;
                player1Stamina.enabled = false;
                player2Stamina.enabled = false;

                player1Wager.enabled = false;
                player2Wager.enabled = false;
                break;
            case GameManager.Phase.RoundEnd:
                roundWinner.enabled = true;
                break;
            case GameManager.Phase.MatchEnd:
                player1Stamina.enabled = false;
                player2Stamina.enabled = false;

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


        player1Stamina.text = $"Player 1 Stamina: {gameManager.Players[0].playerStamina.CurrentStamina}";
        player2Stamina.text = $"Player 2 Stamina: {gameManager.Players[1].playerStamina.CurrentStamina}";

        player1Wager.text = $"Player 1 Wager: {gameManager.Players[0].playerStamina.CurrentWager}";
        player2Wager.text = $"Player 2 Wager: {gameManager.Players[1].playerStamina.CurrentWager}";

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
}
