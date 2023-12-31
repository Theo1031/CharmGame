using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play(){
        SceneManager.LoadScene("SampleScene 1");
    }

    public void Quit(){
        Application.Quit();
    } 

    public void Introduction(){
        SceneManager.LoadScene("Intro");
    }  

    public void Intro(){
        SceneManager.LoadScene("Intro");
    }

    public void Back(){
        SceneManager.LoadScene("Start");
    }
}