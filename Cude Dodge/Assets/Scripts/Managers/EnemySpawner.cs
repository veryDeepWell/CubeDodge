using UnityEngine;
using System.Collections;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    // Var for coroutine
    private IEnumerator _spawnerEnumerator;
    
    // Delay for spawning plane
    [SerializeField] private float spawnDelay;

    [SerializeField] private GameObject[] attackPool;
    
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
    
    private int _randomNumber;
    
    private IEnumerator SpawnerWithTimer()
    {
        Random randomIntForAttack = new Random();

        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            
            Vector3 startPosition = GiveMeRandomPos();
            Vector3 endPosition = GiveMeRandomPos();
            _randomNumber = randomIntForAttack.Next(0, 2);

            switch (_randomNumber)
            {
                case 0:
                    // Plane enemy
                    GameObject planeThingy = Instantiate(attackPool[0], startPosition, Quaternion.identity);
                    
                    planeThingy.GetComponent<EnemyPlane>().StartPosition =  startPosition;
                    planeThingy.GetComponent<EnemyPlane>().EndPosotion =  endPosition;
                    planeThingy.GetComponent<EnemyPlane>().BeginAttack();
                    if (debug) { Debug.Log("[ENEMY_SPAWN] - [SpawnerWithTimer] - Spawned plane, cords: " + startPosition + endPosition); }
                    break;
                case 1:
                    // Laser enemy
                    GameObject laserThingy = Instantiate(attackPool[1], startPosition, Quaternion.identity);
                    
                    laserThingy.GetComponent<EnemyLaser>().StartPosition =  startPosition;
                    laserThingy.GetComponent<EnemyLaser>().EndPosotion =  endPosition;
                    laserThingy.GetComponent<EnemyLaser>().BeginAttack();
                    if (debug) { Debug.Log("[ENEMY_SPAWN] - [SpawnerWithTimer] - Spawned laser, cords: " + startPosition + endPosition); }
                    break;
            }
        }
    }
    
    private Vector3 GiveMeRandomPos()
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
