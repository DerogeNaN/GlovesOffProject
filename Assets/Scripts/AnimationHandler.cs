using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private GameManager gameManager;
    private PlayerController playerController;
    private PlayerController.Actions actionChoice = PlayerController.Actions.None;
    private bool rpsTie;


    private GameManager.Phase oldPhase;

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetPhase() != oldPhase)
        {
            oldPhase = gameManager.GetPhase();
            if (oldPhase == GameManager.Phase.Action)
            {
                animator.SetTrigger("ActionPhaseStart");
            }
        }
    }

    public void ResetActionChoices()
    {
        actionChoice = PlayerController.Actions.None;

    }

    public void SetChoice(PlayerController.Actions choice)
    {
        actionChoice = choice;
    }

    public void SetRPSTie(bool rpsResult)
    {
        rpsTie = rpsResult;
    }
    public void SetResult(GameManager.MatchResult matchResult)
    {
        StringBuilder animationTrigger = new StringBuilder(16);

        switch (actionChoice)
        {
            case PlayerController.Actions.Block:
                animationTrigger.Append("Block");
                break;

            case PlayerController.Actions.Kick:
                animationTrigger.Append("Kick");
                break;

            case PlayerController.Actions.Punch:
                animationTrigger.Append("Punch");
                break;

            default:
                break;
        }

        if (rpsTie)
        {
            animationTrigger.Append("SameAction");
        }

        switch (matchResult)
        {
            case GameManager.MatchResult.Win:
                animationTrigger.Append("Win");
                break;

            case GameManager.MatchResult.Lose:
                animationTrigger.Append("Lose");
                break;

            case GameManager.MatchResult.Tie:
                animationTrigger.Append("Tie");
                break;

            default:
                break;
        }

        animator.SetTrigger(animationTrigger.ToString());
    }

    public void EnableAnimationHandler()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerController = GetComponent<PlayerController>();
        this.enabled = true;
    }

    void EndRound()
    {
        gameManager.EndRound();
    }
}
