using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayersToVcamTargets : MonoBehaviour
{
    [TagField]
    public string Tag = "Player";

    [SerializeField] CinemachineTargetGroup targetGroup;
    private void Start()
    {


    }

}
