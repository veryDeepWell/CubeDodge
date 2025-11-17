using System;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    
    private IEnumerator _spawnerEnumerator;
    
    [SerializeField] private float SpawnDelay;
    
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private GameObject[] spawnObjects;

    private void Awake()
    {
        _spawnerEnumerator = SpawnerWithTimer();
    }

    void Start()
    {
        StartCoroutine(_spawnerEnumerator);
    }

    void Update()
    {
        
    }
    
    private IEnumerator SpawnerWithTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnDelay);

            ForeshadowEnemy();
            
            if (debug) {Debug.Log("Spawn Complete");}
        }
    }

    private void ForeshadowEnemy()
    {
        // Create object for line renderer
        GameObject empyObject = new GameObject("Foreshadow Line");
    
        Vector3 spawnPosition = randomSpawnerPosition();
        Vector3 destinationPosition = randomSpawnerPosition();
    
        //Create line renderer
        LineRenderer lineRenderer = empyObject.AddComponent<LineRenderer>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the color
        Color startColor = Color.red;
        startColor.a = 0.1f;
        lineRenderer.startColor = startColor;
    
        Color endColor = Color.red;
        endColor.a = 0.1f;
        lineRenderer.endColor = endColor;

        // Set the width
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

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
        StartCoroutine(DelayedSpawn(spawnPosition, destinationPosition, instantiatedLine));
    }
    
    private IEnumerator DelayedSpawn(Vector3 spawnPos, Vector3 destPos, GameObject lineObject)
    {
        // Ждем 2 секунды
        yield return new WaitForSeconds(2f);
    
        // Уничтожаем линию
        Destroy(lineObject);
    
        // Спавним врага
        SpawnEnemy(spawnPos, destPos);
    }
    
    private void SpawnEnemy(Vector3 SpawnPosition,  Vector3 DestinationPosition)
    {
        Vector3 spawnPosition = SpawnPosition;
        Vector3 destinationPosition = DestinationPosition;
        
        //Creating enemy
        GameObject createdEnemy = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity, this.transform);
        createdEnemy.GetComponent<Enemy>().desiredPosition = destinationPosition;
    }

    private Vector3 randomSpawnerPosition()
    {
        //Random spawn zone
        int zoneNumber = UnityEngine.Random.Range(0, spawnObjects.Length);
        
        //Random point in spawn zone
        float randomX = UnityEngine.Random.Range(0, spawnObjects[zoneNumber].gameObject.transform.localScale.x);
        float randomY = UnityEngine.Random.Range(0, spawnObjects[zoneNumber].gameObject.transform.localScale.y);
        
        //Vector 3 with random coordinates
        Vector3 spawnPos = new Vector3(
                               randomX - spawnObjects[zoneNumber].gameObject.transform.localScale.x / 2, 
                               randomY - spawnObjects[zoneNumber].gameObject.transform.localScale.y / 2, 
                               1) 
                           + spawnObjects[zoneNumber].transform.position;
        
        return spawnPos;
    }
}
