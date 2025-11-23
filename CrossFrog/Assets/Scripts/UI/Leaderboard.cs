using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Leaderboard : MonoBehaviour
{
    public List<ScoreRecord> scoreRecords;
    private List<int> scoreList;

    #region 获取持久化得分记录数组
    private void OnEnable()
    {
        scoreList = GameManager.instance.GetScoreListData();        
    }
    #endregion

    #region 控制预制体的显示，以及其上的分数
    private void Start()
    {
        SetLeaderboardData();
    }

    public void SetLeaderboardData()
    {
        for (int i = 0; i < scoreRecords.Count; i++)
        {
            if (i < scoreList.Count)
            {
                scoreRecords[i].SetScoreText(scoreList[i]);
                scoreRecords[i].gameObject.SetActive(true);
            }
            else
            {
                scoreRecords[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion
}