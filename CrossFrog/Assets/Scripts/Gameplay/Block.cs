using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
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
}
