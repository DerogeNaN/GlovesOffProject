using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] Image screenFadeImage;
    float alpha;
    bool fadeOut;
    bool fadeIn;

    [SerializeField] float fadeTime;
    //[SerializeField] float delay;

    private void Start()
    {
        
    }

    void Update()
    {
        if (fadeOut)
        {
            for (float currentTime = 0; currentTime <= fadeTime; currentTime += Time.deltaTime)
            {
                alpha = (currentTime / fadeTime);
                screenFadeImage.color = new Color(0, 0, 0, alpha);
            }
        }

        else
        {
            for (float currentTime = fadeTime; currentTime >= 0; currentTime -= Time.deltaTime)
            {
                alpha = (currentTime / fadeTime);
                screenFadeImage.color = new Color(0, 0, 0, alpha);
            }
        }
    }

    public void FadeOutOfScene()
    {
        fadeOut = true;
    }
    public void FadeIntoScene()
    {
        fadeIn = true;
    }
}
