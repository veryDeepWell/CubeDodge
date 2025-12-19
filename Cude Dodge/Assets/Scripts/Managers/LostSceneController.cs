using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LostSceneController : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AdvertiseManager advertiseManager;
    [SerializeField] private float adLoadDelay = 2f;
    
    private void Start()
    {
        // Показываем счёт
        int savedScore = PlayerPrefs.GetInt("playerScore", 0);
        scoreText.text = "Score: " + savedScore;
        
        // Настраиваем кнопки
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        
        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMenu);
        
        // Ждём немного, чтобы реклама успела загрузиться, потом показываем
        StartCoroutine(ShowAdWithDelay());
    }
    
    private IEnumerator ShowAdWithDelay()
    {
        Debug.Log("Ждём загрузки рекламы...");
        
        // Ждём 2 секунды, чтобы SDK успел загрузить рекламу
        yield return new WaitForSeconds(adLoadDelay);
        
        // Теперь показываем
        if (advertiseManager != null)
        {
            Debug.Log("Показываем рекламу на сцене проигрыша");
            advertiseManager.ShowInterstitial();
        }
    }
    
    private void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}