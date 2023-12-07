using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] GameObject[] vfx = new GameObject[3];
    [SerializeField] Transform head, fistR, fistL, footR, footL;
    private GameObject tempDizzy, tempPunch, tempKick;

    public void PlayDizzyFX()
    {
        tempDizzy = Instantiate(vfx[0], head);
    }

    public void PlayPunchLeftFX()
    {
        tempPunch = Instantiate(vfx[1], fistL);
    }

    public void PlayPunchRightFX()
    {
        tempPunch = Instantiate(vfx[1], fistR);
    }

    public void PlayKickLeftFX()
    {
        tempKick = Instantiate(vfx[2], footL);
    }

    public void PlayKickRightFX()
    {
        tempKick = Instantiate(vfx[2], footR);
    }

    public void ClearVFX()
    {
        Destroy(tempDizzy);
        Destroy(tempPunch);
        Destroy(tempKick);
    }
}
