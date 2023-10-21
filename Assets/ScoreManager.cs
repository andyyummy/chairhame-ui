using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private int score = 0;

    public int Score { get => score; set => score = value; }

    public void AddScore()
    {
        Score++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (text != null)
        {
            text.text = $"Score: {Score}";
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not assigned!");
        }
    }
}