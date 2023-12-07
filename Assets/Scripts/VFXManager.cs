using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] GameObject dizzy, impactFist, impactFoot;
    [SerializeField] Transform head, fistR, fistL, footR, footL;
    //GameObject[] instances = new GameObject[2];
    


    //public void OnPunch(int playerIndex, Vector3 position)
    //{
    //    instances[playerIndex] = Instantiate(impactFist, position, Quaternion.identity);
    //    ParticleSystem[] particleSystems = instances[playerIndex].GetComponentsInChildren<ParticleSystem>();
    //    foreach (var sys in particleSystems)
    //    {
    //        sys.Play();
    //    }
    //}

    public void PlayDizzyFX(Transform position)
    {
        Instantiate(dizzy, position);
        ParticleSystem[] particleFX = dizzy.GetComponentsInChildren<ParticleSystem>();
        foreach(var particle in particleFX)
        {
            particle.Play();
        }
    }

    //public void ClearParticles()
    //{
    //    for (int i = 0; i < instances.Length; i++)
    //    {
    //        Destroy(instances[i]);
    //        instances[i] = null;
    //    }
    //}
}
