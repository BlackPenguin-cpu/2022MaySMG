using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject obj;
    public float objSpawnDelay;
    float curObjSpawnDelay;

    public Vector2 spawnPos;
    public void Update()
    {
        curObjSpawnDelay += Time.deltaTime;
        if (curObjSpawnDelay > objSpawnDelay)
        {
            curObjSpawnDelay = 0;
            spawnObj();
        }
    }
    void spawnObj()
    {
        Instantiate(obj, spawnPos, Quaternion.identity);


    }
}
