using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<int> scoreList;
    private int score;
    private string dataPath;

    #region 持久化存储得分和单例模式
    private void Awake()
    {
        //Unity提供的持久化存储路径
        dataPath = Application.persistentDataPath + "/lenderboard.json";
        scoreList = GetScoreListData();

        //单例模式
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }
    #endregion

    #region 获取持久化得分记录数组
    public List<int> GetScoreListData()
    {
        //若持久化数据存在，就读取文件里内容
        //返回携带分数的数组
        if (File.Exists(dataPath))
        {
            string jsonData = File.ReadAllText(dataPath);
            return JsonConvert.DeserializeObject<List<int>>(jsonData);
        }

        //尚无游玩分数记录，就返回空数组
        return new List<int>();
    }
    #endregion

    #region 事件系统 
    private void OnEnable()
    {
        EventHandler.GameOverEvent += OnGameOverEvent;
        EventHandler.GetPointEvent += OnGetPointEvent;
    }


    private void OnDisable()
    {
        EventHandler.GameOverEvent -= OnGameOverEvent;
        EventHandler.GetPointEvent -= OnGetPointEvent;
    }

    //这是事件的回调函数
    //事件传来多少分，就把score更新成多少分
    private void OnGetPointEvent(int point)
    {
        score = point;
    }

    //游戏结束时，更新得分记录
    private void OnGameOverEvent()
    {
        //记录里不存在同分，则添加新分数
        if (!scoreList.Contains(score))
        {
            scoreList.Add(score);
        }

        scoreList.Sort();
        scoreList.Reverse(); //从高到低

        //把数组信息转换成JSON字符串，覆盖持久化存储文件
        File.WriteAllText(dataPath, JsonConvert.SerializeObject(scoreList));
    }
    #endregion 
}