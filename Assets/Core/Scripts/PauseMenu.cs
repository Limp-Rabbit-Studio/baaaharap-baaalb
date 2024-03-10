using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region - Declarations
    public GameObject pauseMenu;
    public GameObject GameOverMenu;
    public bool isPaused;
    private PlayerStats playerStats;
    #endregion

    #region - Initializer
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);

        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats script not found in the scene!");
        }
    }


    #endregion

    #region - Events
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PuaseGame();
        }
        if (playerStats != null && playerStats.isDead)
        {
            GameOver();
            playerStats.isDead = false; // Reset isDead in PlayerStats script
        }
    }
    #endregion

    #region - Methods
    public void PuaseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        CleanUp();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        CleanUp();
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void CleanUp()
    {
    }
}
#endregion
