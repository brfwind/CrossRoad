using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//根据屏幕自动调整正交相机大小的自适应方案，以及摄像机跟随青蛙
public class CameraControl : MonoBehaviour
{
    //获得青蛙位置
    public Transform frog;
    public float offsetY;
    //屏幕宽高比
    private float ratio;
    //基础缩放值
    public float zoomBase;

    #region 相机大小随屏幕比例自动调整方案
    private void Start()
    {
        //计算屏幕宽高比
        ratio = (float)Screen.height / (float)Screen.width;
        //修改正交相机大小
        Camera.main.orthographicSize = zoomBase * ratio * 0.5f;
    }
    #endregion
    
    #region 摄像机跟随青蛙方案
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, frog.transform.position.y + offsetY * ratio, transform.position.z);
    }
    #endregion
}
