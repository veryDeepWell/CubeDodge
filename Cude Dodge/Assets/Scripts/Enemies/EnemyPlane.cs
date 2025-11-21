using System.Collections;
using UnityEngine;

public class EnemyPlane : MonoBehaviour,
    IEnemy
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    // Plane stats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delayBeforeMove;
    
    // Death particle
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem particleInstance;
    
    // Movement start and end
    public Vector3 StartPosition;
    public Vector3 EndPosotion;

    private float counter;
    private bool delayElapsed = false;

    private void Start()
    {
        // Random start rotation
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    private void FixedUpdate()
    {
        // Allow only if elapsed foreshadow delay
        if (!delayElapsed) { return; }
        
        // Movement
        EnemyMove();
        
        // Suicide time!
        counter += Time.deltaTime;
        if (counter >= 10) { Destroy(this.gameObject); }
    }

    public void BeginAttack()
    {
        // Just interlayer for convenience
        ForeshadowEnemy();
    }

    public void ForeshadowEnemy()
    {
        // Create line renderer
        GameObject foreshadowObject = new GameObject("Foreshadow Plane Line");
    
        // Get positions for line renderer
        Vector3 spawnPosition = StartPosition;
        Vector3 destinationPosition = EndPosotion;
    
        // Adding line renderer to object
        LineRenderer lineRenderer = foreshadowObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Begin color
        Color startColor = Color.red;
        startColor.a = 0.1f;
        lineRenderer.startColor = startColor;
        
        // End color
        Color endColor = Color.red;
        endColor.a = 0.05f;
        lineRenderer.endColor = endColor;

        // Line width
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.02f;
        
        // Setting positions
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, spawnPosition);
        lineRenderer.SetPosition(1, destinationPosition);
    
        // Coroutine, I choose you!
        StartCoroutine(DelayedAction(delayBeforeMove, foreshadowObject));
    }

    public IEnumerator DelayedAction(float timeToWait, GameObject foreshadowObject)
    {
        // Wait....
        yield return new WaitForSeconds(timeToWait);
        
        // Suicide!
        if (foreshadowObject != null)
        {
            Destroy(foreshadowObject);
        }
        
        // Allowing flying
        delayElapsed = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (debug) { Debug.Log("Collide detected"); }
        
        // If we flew into player
        if (collision.gameObject.CompareTag("MainHero"))
        {
            collision.gameObject.GetComponentInParent<Player>().HealthDown(5);
            SpawnParticles();
            Destroy(this.gameObject);
        }
    }

    private void SpawnParticles()
    {
        // Just particles, only when we flew into player
        particleInstance = Instantiate(particle, transform.position, Quaternion.identity);
    }

    private void EnemyMove()
    {
        // Just movement
        var step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, EndPosotion, step);
    }
}
