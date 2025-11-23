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

    #region 事件系统 
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
        //让游玩时显示的分数Text等于当前分数
        scoreText.text = Point.ToString();
    }

    private void OnGameOverEvent()
    {
        //GameOver后，显示游戏结束面板
        gameOverPanel.SetActive(true);

        //时间停止流逝
        if (gameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }
    #endregion

    #region 初始化游玩界面
    private void Start()
    {
        bool show = SettingsManager.instance.showControl;

        controlPanel.SetActive(show);

        scoreText.text = "00";
    }
    #endregion

    #region 给各个按钮添加的方法
    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("Gameplay");
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