using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    #region 自动生成的场景销毁逻辑
    void Update()
    {
        CheckPosition();
    }
    private void CheckPosition()
    {
        if (Camera.main.transform.position.y - transform.position.y > 60)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}