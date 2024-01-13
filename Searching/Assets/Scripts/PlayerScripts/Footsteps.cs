using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] AudioSource footsteps;
    [SerializeField] AudioClip footstepLeft, footstepRight;
    
    public void LeftFootsteps()
    {
        footsteps.clip = footstepLeft;
        footsteps.Play();
    }

    public void RightFootstep()
    {
        footsteps.clip = footstepRight;
        footsteps.Play();
    }
}
