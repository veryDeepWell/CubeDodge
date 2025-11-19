using System;
using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Object with sprite renderer
    [SerializeField] private GameObject itsMeDio;
    
    // Color that starts
    [SerializeField] private Color startColor = Color.red;
    // Color that ends
    [SerializeField] private Color endColor = Color.blue;
    // Time to change color from start to end
    [SerializeField] private float duration = 3.0f;
    
    // Parameter shit
    private float parameterMin;
    private float parameterMax;
    
    // Var for renderer component
    private Renderer objectRenderer;

    private void Awake()
    {
        if (itsMeDio == null)
        {
            // If we don't have object to change, we take renderer from this
            objectRenderer = GetComponent<Renderer>();
        }
        else
        {
            // If we have object to change, we take renderer from this object
            objectRenderer = itsMeDio.GetComponent<Renderer>();
            // Made for complex object architecture, when sprite and script are on different objects
        }
    }

    // СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ 
    //    СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ 
    // СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ 
    //    СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ 
    // СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ 
    //    СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ СМЕРТЬ 

    private IEnumerator ChangeWithTime()
    {
        // Time passed
        float elapsedTime = 0.0f;

        // Cycle for color changing
        while (elapsedTime < duration)
        {
            // Color changing code
            float t = elapsedTime / duration;           
            objectRenderer.material.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Set end color just in case
        objectRenderer.material.color = endColor;
    }
    
    public void StartColorChangingWithTime()
    {
        // Starting coroutine
        StartCoroutine(ChangeWithTime()); 
    }

    public void ParameterInit(float min, float max)
    {
        parameterMin = min; // Minimal parameter for end color
        parameterMax = max; // Maximum parameter for start color
        // min-end, max-start = regression, Example - HP
        // min-start, max-end = progression, Example - Poison
    }
    
    public void ChangeWithParameter(float parameter)
    {
        // Clamp  HP in min-max
        parameter = Mathf.Clamp(parameter, parameterMin, parameterMax);
        
        // Normalized HP (from 0 to 1)
        float normalizedHP = (parameter - parameterMin) / (parameterMax - parameterMin);
        
        // Lerp color
        Color targetColor = Color.Lerp(endColor, startColor, normalizedHP);
        
        // Set color
        objectRenderer.material.color = targetColor;
    }
}
