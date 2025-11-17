using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;
    
    public Vector3 desiredPosition;

    private float counter;

    private void Start()
    {
        this.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-6f, 6f);
    }

    void Update()
    {
        EnemyMove();
        counter +=  Time.deltaTime;
        
        if (counter >= 10) {Destroy(this.gameObject);}
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (debug) {Debug.Log("Collide detected");}
        
        if (collision.gameObject.CompareTag("MainHero"))
        {
            collision.gameObject.GetComponentInParent<Player>().HealthDown(5);
            SpawnParticles();
            Destroy(this.gameObject);
        }
    }

    private void SpawnParticles()
    {
        particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
    }

    private void EnemyMove()
    {
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, step);
    }
}
