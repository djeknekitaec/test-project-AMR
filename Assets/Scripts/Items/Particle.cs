using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleSys;

    public void PlayParticle()
    {
        particleSys.Play();
    }
}
