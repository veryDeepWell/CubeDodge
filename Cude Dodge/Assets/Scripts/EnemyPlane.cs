using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyPlane : MonoBehaviour
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    // Plane speed
    [SerializeField] private float moveSpeed;
    // Death particle
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;
    
    // Movement destination we got from spawner
    public Vector3 desiredPosition;

    // I don't remember why this here
    private float counter;

    private void Start()
    {
        // I wanted random rotation, but gave up
        // NVM!
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    void FixedUpdate()
    {
        // One flying step
        EnemyMove();
        // Suicide timers
        counter +=  Time.deltaTime;
        if (counter >= 10) {Destroy(this.gameObject);}
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug shit
        if (debug) {Debug.Log("Collide detected");}
        
        if (collision.gameObject.CompareTag("MainHero"))
        {
            // If we hit player, down his health
            collision.gameObject.GetComponentInParent<Player>().HealthDown(5);
            // Create red particles
            SpawnParticles();
            // KILL MYSELF!!
            Destroy(this.gameObject);
        }
    }

    private void SpawnParticles()
    {
        // Particles UwU
        particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
    }

    private void EnemyMove()
    {
        // (╯°□°)╯︵ ┻━┻
        // One step of enemy movement
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, step);
    }
}
