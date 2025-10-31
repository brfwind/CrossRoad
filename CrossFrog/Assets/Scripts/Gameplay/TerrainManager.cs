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

    public void CheckPosition()
    {
        if (transform.position.y - Camera.main.transform.position.y < offsetY / 2)
        {
            SpawnTerrain();
        }
    }

    private void SpawnTerrain()
    {
        var randomIndex = Random.Range(0, terrainObjects.Count);

        while (lastIndex == randomIndex)
        {
            randomIndex = Random.Range(0, terrainObjects.Count);
        }

        lastSpawnObject = terrainObjects[lastIndex];

        lastIndex = randomIndex;
        spawnObject = terrainObjects[randomIndex];
  

        Terrainhight temp = lastSpawnObject.GetComponent<Terrainhight>();

        currentHeight = temp.hight;

        Vector3 spawnPos;

        if (isFirstSpawn)
        {
            spawnPos = new Vector3(0, 24, 0);
            isFirstSpawn = false;
        } else
        {
            spawnPos = new Vector3(0, transform.position.y + currentHeight, 0);
        }

        Instantiate(spawnObject, spawnPos, Quaternion.identity);

        transform.position = spawnPos;
    }
}
