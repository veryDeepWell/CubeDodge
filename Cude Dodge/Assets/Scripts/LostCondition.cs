using UnityEngine;
using UnityEngine.SceneManagement;

public class LostCondition : MonoBehaviour
{
    [SerializeField] private bool debug = false;

    public void PlayerLost()
    {
        if (debug) Debug.Log("[LOST] - [PlayerLost] - Skill issue");

        SaveScore();

        SceneManager.LoadScene("lost");
    }

    private void SaveScore()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            int score = scoreManager.getScore();
            PlayerPrefs.SetInt("playerScore", score);
            PlayerPrefs.Save();
            if (debug) Debug.Log("[LOST] - [SaveScore] - Сохранён счёт: " + score);
        }
    }
    
    public void PlayerRevive()
    {
        Player player = GetComponent<Player>();
        if (player != null)
        {
            player.HealthSet(player.MaxHealth);
        }
    }
}
