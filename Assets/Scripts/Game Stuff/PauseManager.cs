using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public GameObject inventoryPanel;
    private bool usingInventory;

    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !usingInventory)
        {
            ResumeOrPause();
        }
        if (Input.GetButtonDown("Inventory") && inventoryPanel.activeInHierarchy && !isPaused)
        {
            CloseInventory();
        }
        else if (Input.GetButtonDown("Inventory") && !isPaused)
        {
            OpenInventory();
        }
    }

    void OpenInventory()
    {
        usingInventory = true;
        inventoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void CloseInventory()
    {
        usingInventory = false;
        inventoryPanel.SetActive(false);
        Time.timeScale = 1f;
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
