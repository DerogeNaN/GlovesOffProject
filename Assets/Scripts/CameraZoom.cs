using Cinemachine;
using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;

    bool zoomIn;
    bool zoomOut;

    float defaultFOV;
    [SerializeField] float minFOV;
    [SerializeField] float zoomSpeed;


    float currentTime;
    [SerializeField] float zoomTime;

    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        defaultFOV = cinemachineVirtualCamera.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            zoomIn = true;
        }

        if (zoomIn)
        {
            if (currentTime <= zoomTime)
            {
                currentTime += Time.deltaTime;
                if (cinemachineVirtualCamera.m_Lens.FieldOfView <= minFOV)
                {
                    cinemachineVirtualCamera.m_Lens.FieldOfView = minFOV;
                    return;
                }
                cinemachineVirtualCamera.m_Lens.FieldOfView -= zoomSpeed;
                return;
            }
            currentTime = 0;
            zoomIn = false;
            zoomOut = true;
        }

        if (zoomOut)
        {
            if (cinemachineVirtualCamera.m_Lens.FieldOfView >= defaultFOV)
            {
                cinemachineVirtualCamera.m_Lens.FieldOfView = defaultFOV;
                zoomOut = false;
                return;
            }
            cinemachineVirtualCamera.m_Lens.FieldOfView += zoomSpeed;
        }
    }
    public void ZoomIn()
    {
        currentTime = 0;
        zoomIn = true;
    }
    public void ZoomOut()
    {
        currentTime = zoomTime;
        zoomOut = true;
    }
}
