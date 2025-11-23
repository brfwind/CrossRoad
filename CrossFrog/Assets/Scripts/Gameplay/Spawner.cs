using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int direction;
    //存储可动物体预制体
    public List<GameObject> spawnObjects;

    #region 随机时间间隔，生成随机可动物体
    private void Start()
    {
        //在游戏开始0.2f秒后，随机5-7s的间隔，执行Spawn方法
        InvokeRepeating("Spawn", 0.2f, Random.Range(5f, 7f));
    }

    private void Spawn()
    {
        //随机一个可动物体数组下标
        var index = Random.Range(0, spawnObjects.Count);
        //生成该物体
        var target = Instantiate(spawnObjects[index], transform.position, Quaternion.identity, transform);

        //给生成的物体指定移动方向
        target.GetComponent<MoveForward>().dir = direction;
    }
    #endregion
}