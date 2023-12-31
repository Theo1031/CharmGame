using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    void Start()
    {
        float score = PlayerPrefs.GetFloat("Score");
        float highScore = PlayerPrefs.GetFloat("HighScore");

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }

        scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        highScoreText.text = "High Score: " + Mathf.RoundToInt(highScore).ToString();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
    }

    public void Quit()
    {
        Application.Quit();
    } 

    public void Introduction()
    {
        SceneManager.LoadScene("SampleScene 1");
    }  
}