using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LostCondition : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    private bool weAdvertiseNow = false;
    
    private PlayerManager playerManager;
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void PlayerLost()
    {
        AdvertiseManager advertiseManager = playerManager.Admin.GetComponent<AdvertiseManager>();
        ScoreManager scoreManager = playerManager.Admin.GetComponent<ScoreManager>();
        
        if (weAdvertiseNow) {advertiseManager.AdvertiseStart();}

        ScoreThingis(scoreManager.getScore());
        
        SceneManager.LoadScene("lost");
    }

    private void ScoreThingis(int score)
    {
        PlayerPrefs.SetInt("playerScore", score);
        PlayerPrefs.Save();
    }
    
    public void PlayerRevive()
    {
        Player player = GetComponent<Player>();

        player.HealthSet(player.MaxHealth);
    }
}
