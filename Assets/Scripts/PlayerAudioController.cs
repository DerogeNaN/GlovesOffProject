using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public enum ReactionSound
    {
        boom,
        pow
    }

    public void PlaySound(ReactionSound animationID)
    {
        Debug.Log("Sound as playd!");
    }

}
