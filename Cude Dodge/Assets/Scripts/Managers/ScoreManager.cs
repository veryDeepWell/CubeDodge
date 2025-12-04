using System;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Var for text object
    [SerializeField] private GameObject scoreText;
    
    // Init score        ↓(no shit sherlock)
    private int _score = 0;
       
    public void ScoreAdd(int addAmount)
    {
        // Counting score
        _score += addAmount;
        ScoreUpdate();
    }

    public int getScore()
    {
        return _score;
    }
    
    private void ScoreUpdate()
    {
        // Getting text component
        scoreText.GetComponent<TextMeshPro>().text = _score.ToString();
    }
}
