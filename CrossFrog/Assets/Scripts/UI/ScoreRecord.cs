using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRecord : MonoBehaviour
{
    public Text scoreText;

    #region 修改排行榜栏预制体上的分数文本
    public void SetScoreText(int point)
    {
        scoreText.text = point.ToString();
    }
    #endregion
}