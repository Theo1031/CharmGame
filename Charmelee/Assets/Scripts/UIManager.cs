using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image[] heartIcons;
    public Text scoreText;
    public Text highScoreText;

    private float score;

    void Update()
    {
        UpdateHeartIcons();
        UpdateScore();
    }

    void UpdateHeartIcons()
    {
        int currentHealth = playerHealth.GetCurrentHealth();

        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = i < currentHealth;
        }
    }

    void UpdateScore()
    {
        if (playerHealth.GetCurrentHealth() > 0)
        {
            score += Time.deltaTime;
        }
        else
        {
            PlayerPrefs.SetFloat("Score", score);
            if (score > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", score);
            }
        }
        scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        highScoreText.text = "High Score: " + Mathf.RoundToInt(PlayerPrefs.GetFloat("HighScore")).ToString();
    }
}
