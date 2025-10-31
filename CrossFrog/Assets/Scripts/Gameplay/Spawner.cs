using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int direction;
    public List<GameObject> spawnObjects;

    private void Start()
    {
        InvokeRepeating("Spawn", 0.2f, Random.Range(5f, 7f));
    }

    private void Spawn()
    {
        var index = Random.Range(0, spawnObjects.Count);
        var target = Instantiate(spawnObjects[index], transform.position, Quaternion.identity, transform);

        target.GetComponent<MoveForward>().dir = direction;
    }
}


