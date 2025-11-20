using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLaser : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public void ForeshadowAttack()
    {
        // Create empty object for line renderer
        GameObject empyObject = new GameObject("Foreshadow Line");
    
        // Random spawn pos
        Vector3 spawnPosition = startPos;
        // Random destination pos
        Vector3 destinationPosition = endPos;
    
        // Create line renderer
        LineRenderer lineRenderer = empyObject.AddComponent<LineRenderer>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the colors
        Color startColor = Color.pink;
        startColor.a = 0.1f;
        lineRenderer.startColor = startColor;
        
        Color endColor = Color.pink;
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

        // Запускаем корутину для задержки
        StartCoroutine(DelayedAttack(spawnPosition, destinationPosition, instantiatedLine));
    }
    
    private IEnumerator DelayedAttack(Vector3 spawnPos, Vector3 destPos, GameObject lineObject)
    {
        // Wait for two seconds before DESTRUCTION!
        yield return new WaitForSeconds(1f);
    
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
        // Create empty object for line renderer
        GameObject empyObject = new GameObject("Attack Line");
    
        // Create line renderer
        LineRenderer lineRenderer = empyObject.AddComponent<LineRenderer>();

        // Set the material
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Set the colors
        Color startColor = Color.pink;
        startColor.a = 1f;
        lineRenderer.startColor = startColor;
        
        Color endColor = Color.pink;
        endColor.a = 1f;
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
        
        StartCoroutine(DelayedSomething(instantiatedLine));
    }
    
    private IEnumerator DelayedSomething(GameObject lineObject)
    {
        // Wait for two seconds before DESTRUCTION!
        yield return new WaitForSeconds(1f);
    
        // Destroy red line
        Destroy(lineObject);
    }
}
