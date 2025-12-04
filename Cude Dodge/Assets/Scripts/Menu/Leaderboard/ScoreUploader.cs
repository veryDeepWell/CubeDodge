using UnityEngine;
using TMPro;

using UnityEngine.Events;

public class ScoreUploader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_InputField nameText;
    
    public UnityEvent<string, int> SubmitScore;
    
    public void SendScore()
    {
        SubmitScore.Invoke(nameText.text, int.Parse(scoreText.text));
    }
}
