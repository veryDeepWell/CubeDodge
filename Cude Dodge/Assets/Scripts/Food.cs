using System;
using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    // Particle shit
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;

    private void Start()
    {
        // Starting fading thing
        GetComponent<ColorChanger>().StartColorChangingWithTime();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug shit
        if (debug) {Debug.Log("Collide detected");}
        
        if (collision.gameObject.CompareTag("MainHero"))
        {
            // If we collide with player - give him health
            collision.gameObject.GetComponentInParent<Player>().HealthUp(5);
            SpawnParticles(); // And spawn particles
            
            // Add score to player
            ScoreManager scoreManager = collision.gameObject.GetComponentInParent<PlayerManager>().Admin.GetComponent<ScoreManager>();
            scoreManager.ScoreAdd(5);
            
            // KILL MYSELF!!!
            Destroy(this.gameObject);
        }
    }
    
    private void SpawnParticles()
    {
        // Just particles OwO
        particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
    }
}
