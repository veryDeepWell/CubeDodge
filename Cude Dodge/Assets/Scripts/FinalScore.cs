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

        if (scoreText == null) { Debug.Log("[SCORE] - FinalScore.cs - Score text is null"); }
        if (debug) { Debug.Log("[SCORE] - FinalScore.cs - Saved score = " + loadedScore); }

        scoreText.GetComponent<TextMeshProUGUI>().text = loadedScore.ToString();
    }
}
