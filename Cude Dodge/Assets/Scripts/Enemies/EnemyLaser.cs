using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyLaser : MonoBehaviour,
    IEnemy
{
    // Debug flag
    [SerializeField] private bool debug = false;
    
    [SerializeField] private float delayBeforeMove;
    [SerializeField] private float delayBeforeSuicide;
    
    // Movement start and end
    public Vector3 StartPosition;
    public Vector3 EndPosotion;
    
    public void BeginAttack()
    {
        ForeshadowEnemy();
    }

    public void ForeshadowEnemy()
    {
        // Создаем объект для LineRenderer напрямую в сцене
        GameObject foreshadowObject = new GameObject("Foreshadow Plane Line");
    
        Vector3 spawnPosition = StartPosition;
        Vector3 destinationPosition = EndPosotion;
    
        LineRenderer lineRenderer = foreshadowObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        Color startColor = Color.pink;
        startColor.a = 0.1f;
        lineRenderer.startColor = startColor;
        
        Color endColor = Color.pink;
        endColor.a = 0.1f;
        lineRenderer.endColor = endColor;

        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, spawnPosition);
        lineRenderer.SetPosition(1, destinationPosition);
    
        // Запускаем корутину с созданным объектом
        StartCoroutine(DelayedAction(delayBeforeMove, foreshadowObject));
    }

    public IEnumerator DelayedAction(float timeToWait, GameObject foreshadowObject)
    {
        // Ждем указанное время
        yield return new WaitForSeconds(timeToWait);

        // Уничтожаем LineRenderer
        if (foreshadowObject != null) { Destroy(foreshadowObject); }
        
        MainAttack(); 
    }

    private void MainAttack()
    {
        // Создаем объект для LineRenderer напрямую в сцене
        GameObject attackObject = new GameObject("Foreshadow Plane Line");
    
        Vector3 spawnPosition = StartPosition;
        Vector3 destinationPosition = EndPosotion;
    
        LineRenderer lineRenderer = attackObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        Color startColor = Color.deepPink;
        startColor.a = 1f;
        lineRenderer.startColor = startColor;
        
        Color endColor = Color.deepPink;
        endColor.a = 1f;
        lineRenderer.endColor = endColor;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, spawnPosition);
        lineRenderer.SetPosition(1, destinationPosition);
        
        PerformLinecast();

        StartCoroutine(DelayedDestruction(delayBeforeSuicide, attackObject));
    }
    
    public IEnumerator DelayedDestruction(float timeToWait, GameObject foreshadowObject)
    {
        // Ждем указанное время
        yield return new WaitForSeconds(timeToWait);

        // Уничтожаем LineRenderer
        if (foreshadowObject != null) { Destroy(foreshadowObject); }
        
        Destroy(this.gameObject);
    }
    
    private void PerformRaycast()
    {
        //if (debug) { Debug.Log("I am alive - "); }
        
        Vector3 direction = (EndPosotion - StartPosition).normalized;
        float distance = Vector3.Distance(StartPosition, EndPosotion);
        
        RaycastHit[] hits = Physics.RaycastAll(StartPosition, direction, distance);

        foreach (RaycastHit hit in hits)
        {
            if (debug) { Debug.Log("I am hit - " + hit.transform.gameObject.name); }
            
            if (hit.collider.gameObject.tag == "MainHero")
            {
                hit.collider.GetComponent<Player>().HealthDown(5);
                break;
            }
        }
    }
    
    private void PerformLinecast()
    {
        //if (debug) { Debug.Log("I am alive - "); }
        //if (debug) { Debug.Log("I am hit - " + hit.transform.gameObject.name); }
        
        RaycastHit2D hit = Physics2D.Linecast(StartPosition, EndPosotion);
        
        if (hit.collider != null)
        {
            string hitName = hit.collider.gameObject.name;
        
            if (debug) 
            { 
                Debug.Log($"Linecast hit: {hitName} at distance {hit.distance}");
                Debug.DrawLine(StartPosition, EndPosotion, Color.red, 1f); // Визуализация в Scene view
            }

            if (debug)
            {
                Debug.Log("I am hit - " + hitName);
            }

            if (hit.collider.gameObject.tag == "MainHero")
            {
                hit.collider.gameObject.GetComponentInParent<Player>().HealthDown(5);
            }
        }
        else if (debug)
        {
            Debug.Log("Linecast missed");
            Debug.DrawLine(StartPosition, EndPosotion, Color.green, 1f);
        }
    }
}
