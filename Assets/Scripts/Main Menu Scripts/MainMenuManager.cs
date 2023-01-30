using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] string startSceneName;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject mainMenu;

    public void QuitegameFromMenu()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }
    public void MainMenu()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGameScene()
    {
        SceneManager.LoadScene(startSceneName);
    }
}
