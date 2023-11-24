using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    /// <summary>
    /// Calculated in seconds
    /// </summary>
    public float totalTime;

    private float currentTime;
    public float CurrentTime { get { return currentTime; } }
    private bool isCounting = false;
    private bool isZero = false;

    void Awake()
    {
        isCounting = false;
        currentTime = 0;
    }


    void Update()
    {
        if (!isCounting)
        {
            return;
        }
        currentTime -= Time.deltaTime;
        if (currentTime <= 0.0f)
        {
            currentTime = 0.0f;
            isCounting = false;
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

    public void RestartClock()
    {
        ResetClock();
        StartClock();
    }

    public bool IsZero()
    {
        return isZero;
    }
}
