using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region - Declarations
    public GameObject pauseMenu;
    public bool isPaused;
    #endregion

    #region - Initializer
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
    }
    #endregion

    #region - Events
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) { ResumeGame(); }
            else { PuaseGame(); }
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

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("0 MainMenu");
    }

    public void QuitGame()
    { 
    Application.Quit();
    }
}
#endregion
