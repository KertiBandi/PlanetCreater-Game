using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] string restartSceneName;
    [SerializeField] string nextSceneName;
    [SerializeField] string backToMainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject asteroids;
    public bool isPaused;
    public bool begining = true;


    private void Start()
    {
        isPaused = true;
        Time.timeScale = 0f;

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !begining)
        {
            if (isPaused)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(restartSceneName);
    }

    public void PauseGame()
    {
        settings?.SetActive(false);
        pauseMenu?.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnPauseGame()
    {
        pauseMenu?.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quitegame()
    {
        Application.Quit();
    }

    public void StartNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(backToMainMenu);
    }

    public void StartLevel()
    {
        startMenu?.SetActive(false);
        begining = false;
        asteroids.SetActive(true);
        UnPauseGame();
    }

    public void Settings()
    {
        settings?.SetActive(true);
        pauseMenu?.SetActive(false);
    }

}
