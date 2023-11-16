using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    bool fadeOut;
    bool fadeIn;
    bool isCounting;
    [SerializeField] Image screenFadeImage;
    float screenFadeAlpha;

    public float transitionTime;
    public float delay;
    private float currentTime;
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    void OnEnable()
    {
        currentTime = 0;
        isCounting = true;
        fadeOut = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isCounting)
            return;
        currentTime += Time.deltaTime;

        if (fadeOut)
        {
            if (currentTime >= transitionTime + delay)
            {
                fadeOut = false;
                fadeIn = true;
                currentTime = 0;
            }
            screenFadeAlpha = (currentTime / transitionTime);
            screenFadeImage.color = new Color( 0, 0, 0, screenFadeAlpha);
        }
        if (fadeIn)
        {
            if (currentTime >= transitionTime)
            {
                currentTime = transitionTime;
                fadeIn = false;
                isCounting = false;
                this.enabled = false;
            }
            screenFadeAlpha = 1 - ((currentTime / transitionTime));
            screenFadeImage.color = new Color(0, 0, 0, screenFadeAlpha);
        }
    }

    public void Fade()
    {
        this.enabled = true;
    }
}
