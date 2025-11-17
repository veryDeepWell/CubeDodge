using System;
using System.Collections;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;

    private void Start()
    {
        GetComponent<ColorChanger>().StartColorChangingWithTime();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (debug) {Debug.Log("Collide detected");}
        
        if (collision.gameObject.CompareTag("MainHero"))
        {
            collision.gameObject.GetComponentInParent<Player>().HealthUp(5);
            SpawnParticles();
            
            ScoreManager scoreManager = collision.gameObject.GetComponentInParent<PlayerManager>().Admin.GetComponent<ScoreManager>();
            scoreManager.ScoreAdd(5);
            
            Destroy(this.gameObject);
        }
    }
    
    private void SpawnParticles()
    {
        particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
    }
}
