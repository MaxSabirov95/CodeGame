using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject creditsPanel;
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject pausePanel;

    private void Start()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }

        if (pauseButton != null)
        {
            pauseButton.gameObject.SetActive(true);
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    public void GoToChooseLevels()
    {
        SceneManager.LoadScene("Levels");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void GoToAchievments()
    {
        SceneManager.LoadScene("Achievments");
    }
    public void GoToSettingsPanel()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    public void GoToCreditsPanel()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void ExitToMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        ResumeButton();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void LevelTutorial()
    {
        SceneManager.LoadScene(3);
    }
    public void Level1()
    {
        SceneManager.LoadScene(4);
    }
    public void Level2()
    {
        SceneManager.LoadScene(5);
    }
    public void Level3()
    {
        SceneManager.LoadScene(6);
    }
    public void Level4()
    {
        SceneManager.LoadScene(7);
    }

    private void Awake()
    {
        BlackBoard.scenesManager = this;
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
