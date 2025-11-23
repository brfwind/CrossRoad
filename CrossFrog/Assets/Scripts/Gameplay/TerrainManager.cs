using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public float offsetY;
    public List<GameObject> terrainObjects;
    private GameObject spawnObject;
    private GameObject lastSpawnObject;
    private int currentHeight;
    private int lastIndex;
    private bool isFirstSpawn = true;

    #region 事件板块 
    private void OnEnable()
    {
        EventHandler.GetPointEvent += OnGetPointEvent;
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
    }

    private void OnGetPointEvent(int obj)
    {
        CheckPosition();
    }

    //检测需要新增地形预制体的条件，并调用生成预制体方法
    public void CheckPosition()
    {
        if (transform.position.y - Camera.main.transform.position.y < offsetY / 2)
        {
            SpawnTerrain();
        }
    }
    #endregion

    #region 随机生成地形预制体
    private void SpawnTerrain()
    {
        //随机获得一个地形预制体的下标
        var randomIndex = Random.Range(0, terrainObjects.Count);

        //保证不出现连续两个地形预制体
        while (lastIndex == randomIndex)
        {
            randomIndex = Random.Range(0, terrainObjects.Count);
        }

        lastSpawnObject = terrainObjects[lastIndex];

        lastIndex = randomIndex;
        spawnObject = terrainObjects[randomIndex];
  
        //得到上一个地形预制体的长度信息
        Terrainhight temp = lastSpawnObject.GetComponent<Terrainhight>();
        currentHeight = temp.hight;

        Vector3 spawnPos;

        //计算出放置地形预制体的位置
        if (isFirstSpawn)
        {
            spawnPos = new Vector3(0, 24, 0);
            isFirstSpawn = false;
        } else
        {
            spawnPos = new Vector3(0, transform.position.y + currentHeight, 0);
        }

        //放置新的地形预制体
        Instantiate(spawnObject, spawnPos, Quaternion.identity);

        transform.position = spawnPos;
    }
    #endregion
}