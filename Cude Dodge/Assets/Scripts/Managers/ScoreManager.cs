using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreText;
    
    private int _score = 0;

    public void ScoreUpdate()
    {
        scoreText.GetComponent<TextMeshPro>().text = _score.ToString();
    }
    
    public void ScoreAdd(int addAmount)
    {
        _score += addAmount;
        ScoreUpdate();
    }
}
