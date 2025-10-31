using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Leaderboard : MonoBehaviour
{
    public List<ScoreRecord> scoreRecords;
    private List<int> scoreList;

    private void OnEnable()
    {
        scoreList = GameManager.instance.GetScoreListData();        
    }

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
}
