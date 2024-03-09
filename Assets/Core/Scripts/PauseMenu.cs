using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void QuitGame()
    { 
    Application.Quit();
    }
}
#endregion
