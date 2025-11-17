using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] public ParticleSystem particle;
    
    [Range(0,0.2f)]
    [SerializeField] private float formationPeriod;
    
    private float counter;
    
    public void PlayParticle()
    {
        particle.Play();
    }
}
