using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    // Camera for food spawn position
    [SerializeField] private Camera mainCamera;
    
    // Food prefab
    [SerializeField] private GameObject foodPrefab;

    // Var for coroutine
    private IEnumerator _spawnFood;
    
    private void Awake()
    {
        // Fill coroutine var with coroutine
        _spawnFood =  SpawnFood();
    }
    
    // Компьютеры работают на шестеренках
    
    private void Start()
    {
        // Protection from some shit
        if (mainCamera == null)
        {
            // Get camera, please
            mainCamera = Camera.main;
            
            // Last protection
            if (mainCamera == null)
            {
                Debug.LogError("No MainCamera found");
                return;
            }
        }
        
        // Starting coroutine
        StartCoroutine(_spawnFood);
    }

    private IEnumerator SpawnFood()
    {
        // Creating var for some cases
        GameObject createdEnemy;
        
        while (true)
        {
            // Waiting between spawning food
            yield return new WaitForSeconds(3f);
            
            // Debug thing, ignore it
            if (debug) {Debug.Log("Spawning food");}
            
            // Slightly less zone, for not spawning food at corner of screen
            Vector3 randomViewportPoint = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 1f);
            Vector3 randomWorldPoint = mainCamera.ViewportToWorldPoint(randomViewportPoint);
            
            // Creating food
            createdEnemy = Instantiate(foodPrefab, randomWorldPoint, Quaternion.identity, this.transform);
        }
    }
}
