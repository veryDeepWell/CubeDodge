using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    // Var for coroutine
    private IEnumerator _spawnerEnumerator;
    
    // Delay for spawning plane
    [SerializeField] private float SpawnDelay;
    
    // Prefab for plane
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private GameObject laserPrefab;
    
    // Spawner zones
    [SerializeField] private GameObject[] spawnObjects;

    private void Awake()
    {
        // Fill coroutine var with coroutine
        _spawnerEnumerator = SpawnerWithTimer();
    }

    void Start()
    {
        // Starting coroutine
        StartCoroutine(_spawnerEnumerator);
    }
    
    private IEnumerator SpawnerWithTimer()
    {
        while (true)
        {
            // Delay between enemy spawn
            yield return new WaitForSeconds(SpawnDelay);
            ForeshadowEnemy();
            
            
            // Show red line
            // ONE PIECE IS REAL!!!!
            int randAttack = UnityEngine.Random.Range(0, 2);

            switch (randAttack)
            {
                case 0:
                    break;
                    // Debug thing
                    if (debug) {Debug.Log("Plane enemy spawned");}
                    
                    ForeshadowEnemy();
                    break;
                case 1:
                    break;
                    // Debug thing
                    if (debug) {Debug.Log("Laser enemy spawned");}

                    laserPrefab.GetComponent<EnemyLaser>().startPos = randomSpawnerPosition();
                    laserPrefab.GetComponent<EnemyLaser>().endPos = randomSpawnerPosition();
                    laserPrefab.GetComponent<EnemyLaser>().ForeshadowAttack();
                    break;
            }
        }
    }

    private void ForeshadowEnemy()
    {
        // Create empty object for line renderer
        GameObject empyObject = new GameObject("Foreshadow Line");
    
        // Random spawn pos
        Vector3 spawnPosition = randomSpawnerPosition();
        // Random destination pos
        Vector3 destinationPosition = randomSpawnerPosition();
    
        // Create line renderer
        LineRenderer lineRenderer = empyObject.AddComponent<LineRenderer>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the colors
        Color startColor = Color.red;
        startColor.a = 0.1f;
        lineRenderer.startColor = startColor;
        
        Color endColor = Color.red;
        endColor.a = 0.1f;
        lineRenderer.endColor = endColor;

        // Set the width
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.02f;

        // Set the number of vertices
        lineRenderer.positionCount = 2;
    
        // Set the positions of the vertices
        lineRenderer.SetPosition(0, spawnPosition);
        lineRenderer.SetPosition(1, destinationPosition);
    
        // Instantiate line object и сохраняем ссылку
        GameObject instantiatedLine = Instantiate(empyObject, new Vector3(0, 0, 0), Quaternion.identity);

        // Уничтожаем оригинальный объект (шаблон)
        Destroy(empyObject);

        // Запускаем корутину для задержки
        StartCoroutine(DelayedAttack(spawnPosition, destinationPosition, instantiatedLine));
    }
    
    private IEnumerator DelayedAttack(Vector3 spawnPos, Vector3 destPos, GameObject lineObject)
    {
        // Wait for two seconds before DESTRUCTION!
        yield return new WaitForSeconds(2f);
    
        // Destroy red line
        Destroy(lineObject);
    
        // Spawn enemy himself
        SpawnAttack(spawnPos, destPos);
    }
    
    private void SpawnAttack(Vector3 SpawnPosition,  Vector3 DestinationPosition)
    {
        // Get positions from line
        Vector3 spawnPosition = SpawnPosition;
        Vector3 destinationPosition = DestinationPosition;
        
        // Creating enemy
        GameObject createdEnemy = Instantiate(planePrefab, spawnPosition, Quaternion.identity, this.transform);
        createdEnemy.GetComponent<EnemyPlane>().desiredPosition = destinationPosition;
    }

    private Vector3 randomSpawnerPosition()
    {
        // Random spawn zone
        int zoneNumber = UnityEngine.Random.Range(0, spawnObjects.Length);
        
        // Random point in spawn zone
        float randomX = UnityEngine.Random.Range(0, spawnObjects[zoneNumber].gameObject.transform.localScale.x);
        float randomY = UnityEngine.Random.Range(0, spawnObjects[zoneNumber].gameObject.transform.localScale.y);
        
        // Vector 3 with random coordinates
        Vector3 spawnPos = new Vector3(
                               randomX - spawnObjects[zoneNumber].gameObject.transform.localScale.x / 2, 
                               randomY - spawnObjects[zoneNumber].gameObject.transform.localScale.y / 2, 
                               1) 
                           + spawnObjects[zoneNumber].transform.position;
        
        // Take me, my Helltaker
        return spawnPos;
    }
}
