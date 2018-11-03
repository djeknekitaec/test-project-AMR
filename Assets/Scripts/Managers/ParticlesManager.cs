using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{

    [SerializeField]
    ParticlesPool particlesPool;

    private void Awake()
    {
        particlesPool.Initialize(7);
    }

    public void ShowParticle(Vector3 pos)
    {
        StartCoroutine(showParticle(pos));
    }

    IEnumerator showParticle(Vector3 pos)
    {
        yield return new WaitForEndOfFrame();
        Particle particle = particlesPool.Pull();
        pos.z = particle.transform.position.z;
        particle.transform.position = pos;
        particle.transform.localPosition += new Vector3(0f, 0f, -200f);
        particle.PlayParticle();
        yield return new WaitForSeconds(2f);
        particlesPool.Push(particle);
        yield return null;
    }
}
