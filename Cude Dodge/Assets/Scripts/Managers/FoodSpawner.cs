using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject foodPrefab;

    private IEnumerator _spawnFood;
    
    private void Awake()
    {
        _spawnFood =  SpawnFood();
    }

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            
            if (mainCamera == null)
            {
                Debug.LogError("No MainCamera found");
                return;
            }
        }
        
        StartCoroutine(_spawnFood);
    }

    private IEnumerator SpawnFood()
    {
        GameObject createdEnemy;
        
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (debug) {Debug.Log("Spawning food");}
            
            Vector3 randomViewportPoint = new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 1f);
            Vector3 randomWorldPoint = mainCamera.ViewportToWorldPoint(randomViewportPoint);
            
            createdEnemy = Instantiate(foodPrefab, randomWorldPoint, Quaternion.identity, this.transform);
        }
    }
}
