using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    
    [SerializeField] private GameObject scoreText;
    
    private string _publicKey = "e7d63394c6ba35bdef2299bd2d5db3aa79011bb3ab5e161baedd6947b0249820";

    private void Start()
    {
        GetLeaderboard();
        scoreText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("playerScore").ToString();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(_publicKey, ((msg) =>
        {
            int loopHeight = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopHeight; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(_publicKey, username, score, ((msg) =>
        {
            GetLeaderboard();
        }));
    }
}
