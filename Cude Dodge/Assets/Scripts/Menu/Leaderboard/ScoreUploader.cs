using UnityEngine;
using TMPro;

using UnityEngine.Events;

public class ScoreUploader : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_InputField nameText;
    
    // Ивент для отправки
    public UnityEvent<string, int> SubmitScore;
    
    public void SendScore()
    {
        // Отправляемся в интренет
        SubmitScore.Invoke(nameText.text, int.Parse(scoreText.text));
    }
}
