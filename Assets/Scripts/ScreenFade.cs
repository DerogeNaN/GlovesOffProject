using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] Image screenFadeImage;
    float alpha;
    bool fadeOut;
    bool fadeIn;
    bool isFading;
    public bool IsFading { get { return isFading; } }
    float currentTime;
    [SerializeField] float fadeTime;
    //[SerializeField] float delay;

    private void Start()
    {
        
    }

    void Update()
    {
        if (fadeOut)
        {
            if (currentTime <= fadeTime)
            {
                currentTime += Time.deltaTime;
                alpha = (currentTime / fadeTime);
                screenFadeImage.color = new Color(0, 0, 0, alpha);
                return;
            }
            currentTime = 0;
            fadeOut = false;
            isFading = false;
        }

        if (fadeIn)
        {
            if (currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
                alpha = (currentTime / fadeTime);
                screenFadeImage.color = new Color(0, 0, 0, alpha);
                return;
            }
            currentTime = fadeTime;
            fadeIn = false;
            isFading = false;
        }
    }

    public void FadeOutOfScene()
    {
        fadeOut = true;
        isFading = true;
    }
    public void FadeIntoScene()
    {
        fadeIn = true;
        isFading = true;
    }
}
