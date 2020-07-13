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

    private void Start()
    {
        if (mainMenuPanel == null)
        {
            mainMenuPanel = null;
        }
        else
        {
            mainMenuPanel.SetActive(true);
        }

        if (settingsPanel == null)
        {
            settingsPanel = null;
        }
        else
        {
            settingsPanel.SetActive(false);
        }

        if (creditsPanel == null)
        {
            creditsPanel = null;
        }
        else
        {
            creditsPanel.SetActive(false);
        }
    }

    public void GoToChooseLevels()
    {
        SceneManager.LoadScene(6);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(5);
    }
    public void GoToAchievments()
    {
        SceneManager.LoadScene(7);
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

    public void LevelTutorial()
    {
        SceneManager.LoadScene(0);
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
    }
    public void Level4()
    {
        SceneManager.LoadScene(4);
    }
}
