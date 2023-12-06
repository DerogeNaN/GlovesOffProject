using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;
    GameManager gameManager;
    CameraZoom cameraZoom;
    CameraShake cameraShake;
    PlayerController playerController;
    PlayerController.Actions actionChoice = PlayerController.Actions.None;
    bool rpsTie;

    GameManager.Phase oldPhase;

    public void EnableAnimationHandler()
    {
        enabled = true;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cameraZoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraZoom>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (gameManager.CurrentPhase != oldPhase)
        {
            oldPhase = gameManager.CurrentPhase;
            if (oldPhase == GameManager.Phase.Action)
            {
                animator.SetTrigger("ActionPhaseStart");
            }
            if (oldPhase == GameManager.Phase.RoundBuffer)
            {
                animator.SetTrigger("RoundRestart");
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

    void EndRound()
    {
        gameManager.GoToRoundEnd();
    }
    public void SmallImpactEffect()
    {
        float impactEffectTime = 0.5f;

        cameraZoom.minFOV = 50;
        cameraZoom.zoomSpeed = 1;

        cameraZoom.zoomTime = impactEffectTime;
        cameraZoom.ZoomIn();

        cameraShake.shakeIntensity = 1.0f;
        cameraShake.Shake(impactEffectTime);
        animator.speed = 0;
        Invoke(nameof(ResumeAnimation), impactEffectTime);
    }
    public void BigImpactEffect()
    {
        float impactEffectTime = 1.5f;

        cameraZoom.minFOV = 35;
        cameraZoom.zoomSpeed = 2;

        cameraZoom.zoomTime = impactEffectTime;
        cameraZoom.ZoomIn();

        cameraShake.shakeIntensity = 4.0f;
        cameraShake.Shake(impactEffectTime);
        animator.speed = 0;
        Invoke(nameof(ResumeAnimation), impactEffectTime);
    }
    public void ResumeAnimation()
    {
        animator.speed = 1;
    }
}
