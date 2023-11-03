using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    /// <summary>
    /// Calculated in seconds
    /// </summary>
    public float totalTime;

    public float currentTime;
    private bool isCounting = false;
    private bool isZero = false;

    void Awake()
    {
        isCounting = false;
        currentTime = 0;
        clockText.text = "";
    }


    void Update()
    {
        if (!isCounting)
        {
            return;
        }
        currentTime -= Time.deltaTime;
        UpdateTimerText();
        if (currentTime <= 0.0f)
        {
            currentTime = 0.0f;
            isCounting = false;
            clockText.text = "";
            isZero = true;
        }
    }
    public void StartClock()
    {
        isCounting = true;
    }

    public void StopClock()
    {
        isCounting = false;
    }

    public void ResetClock()
    {
        isZero = false;
        currentTime = totalTime;
    }

    public bool IsZero()
    {
        return isZero;
    }

    private void UpdateTimerText()
    {
        clockText.text = ((int)currentTime + 1).ToString();
    }
}
