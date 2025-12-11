using System;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    
    [SerializeField] private GameObject scoreText;
    
    private void Start()
    {
        LoadScoreFromMemory();
    }

    public void LoadScoreFromMemory()
    {
        int loadedScore = PlayerPrefs.GetInt("playerScore");

        if (scoreText == null) { Debug.Log("[SCORE] - [LoadScoreFromMemory] - Score text object is null"); }
        if (debug) { Debug.Log("[SCORE] - [LoadScoreFromMemory] - Saved score = " + loadedScore); }

        scoreText.GetComponent<TextMeshProUGUI>().text = loadedScore.ToString();
    }
}
