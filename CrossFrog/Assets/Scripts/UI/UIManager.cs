using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject leaderboardPanel;
    public GameObject controlPanel;

    private void OnEnable()
    {
        Time.timeScale = 1;

        EventHandler.GetPointEvent += OnGetPointEvent;
        EventHandler.GameOverEvent += OnGameOverEvent;
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }

    private void OnGetPointEvent(int Point)
    {
        scoreText.text = Point.ToString();
    }

    private void OnGameOverEvent()
    {
        gameOverPanel.SetActive(true);

        if (gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    #region 按钮添加方法
    public void RestartGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        gameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("Gameplay");
    }

    private void Start()
    {
        bool show = SettingsManager.instance.showControl;

        controlPanel.SetActive(show);

        scoreText.text = "00";
    }

    public void BackToMenu()
    {
        gameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("Title");
    }

    public void OpenLeaderBoard()
    {
        leaderboardPanel.SetActive(true);
        controlPanel.SetActive(false);
    }

    public void BackToGame()
    {
        leaderboardPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

#endregion
}
