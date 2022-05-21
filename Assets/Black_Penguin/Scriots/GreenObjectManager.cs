using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenObjectManager : MonoBehaviour
{
    public class YposRange
    {
        public int minimum;
        public int maximum;
    }

    static public GreenObjectManager instance;

    public GameObject floatObj;
    public GameObject jumpObj;
    public float greenGaugeValue;
    public float objSpawnDelay;
    float curObjSpawnDelay;

    public Vector2 spawnPos;
    public YposRange randomYposRange;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);
    }
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
        Instantiate(Random.Range(0, 2) == 0 ? jumpObj : floatObj, spawnPos + Vector2.up * Random.Range(randomYposRange.minimum, randomYposRange.maximum), Quaternion.identity);
    }
}
