using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ResumeOrPause();
        }
    }

    public void ResumeOrPause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        switch (isPaused)
        {
            case true:
                Time.timeScale = 0f;
                break;
            case false:
                Time.timeScale = 1f;
                break;
        }
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
