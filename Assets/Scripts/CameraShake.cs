using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Clock shakeTimer;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    float shakeIntensity = 1f;
    CinemachineBasicMultiChannelPerlin cBMCP;

    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cBMCP = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        StopShake();
    }

    void Update()
    {
        if (shakeTimer.IsZero())
        {
            StopShake();
        }
    }

    public void Shake(float totalTime)
    {
        shakeTimer.totalTime = totalTime;
        cBMCP.m_AmplitudeGain = shakeIntensity;
        shakeTimer.RestartClock();
    }

    public void StopShake()
    {
        cBMCP.m_AmplitudeGain = 0;
    }
}
