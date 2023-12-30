using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // To use Text
using UnityEngine.SceneManagement; // To use SceneManager

public class EndMenu : MonoBehaviour
{
    public Text scoreText; // Reference to the score text
    public Text highScoreText; // Reference to the high score text

    // Start is called before the first frame update
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