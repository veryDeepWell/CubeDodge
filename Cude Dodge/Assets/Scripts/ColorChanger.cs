using System;
using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private GameObject itsMeDio;
    
    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color endColor = Color.blue;
    [SerializeField] private float duration = 3.0f;
    
    private float parameterMin;
    private float parameterMax;
    
    private Renderer objectRenderer;

    private void Awake()
    {
        if (itsMeDio == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }
        else
        {
            objectRenderer = itsMeDio.GetComponent<Renderer>();
        }
    }

    private IEnumerator ChangeWithTime()
    {
        float elapsedTime = 0.0f;                       // Time passed

        while (elapsedTime < duration)                  // Cycle for color changing
        {
            float t = elapsedTime / duration;           // Вычисляем текущее значение t (от 0 до 1)
            objectRenderer.material.color = Color.Lerp(startColor, endColor, t);    // Применяем плавную интерполяцию цвета
            elapsedTime += Time.deltaTime;              // Увеличиваем время, прошедшее с начала цикла
            yield return null;                          // Ждем следующего кадра
        }
        
        objectRenderer.material.color = endColor;       // Setting end color
    }
    
    public void StartColorChangingWithTime()
    {
        StartCoroutine(ChangeWithTime()); 
    }

    public void ParameterInit(float min, float max)
    {
        parameterMin = min;
        parameterMax = max;
    }
    
    public void ChangeWithParameter(float parameter)
    {
        // Ограничиваем HP в пределах min-max
        parameter = Mathf.Clamp(parameter, parameterMin, parameterMax);
        
        // Вычисляем нормализованное значение HP (от 0 до 1)
        float normalizedHP = (parameter - parameterMin) / (parameterMax - parameterMin);
        
        // Интерполируем цвет между maxHPColor и minHPColor
        Color targetColor = Color.Lerp(endColor, startColor, normalizedHP);
        
        // Применяем цвет к материалу
        objectRenderer.material.color = targetColor;
    }
}
