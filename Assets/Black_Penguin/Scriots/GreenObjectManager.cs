using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GreenObjectManager : MonoBehaviour
{
    [System.Serializable]
    public class Range
    {
        public int minimum;
        public int maximum;
    }

    static public GreenObjectManager instance;

    public GameObject floatObj;
    public GameObject jumpObj;
    public float greenGaugeValue;
    public float objSpawnDelay;
    public bool isClicked;

    private int comboCount;
    public int _comboCount
    {
        get { return comboCount; }
        set
        {
            if (value > comboCount)
                comboResetTime = 1;

            comboCount = value;
        }
    }
    public float comboResetTime;
    public TextMeshProUGUI comboText;

    public Vector2 floatSpawnPos;
    public Vector2 jumpSpawnPos;
    public Range randomYposRange;
    public Range jumpBallspawnPos;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);
    }
    private void Start()
    {
        InvokeRepeating("spawnObj", 2.5f, 2.5f);
        InvokeRepeating("spawnJumpObj", 0.35f, 0.35f);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            isClicked = true;
        if (Input.GetMouseButtonUp(0))
            isClicked = false;

        comboResetTime -= Time.deltaTime;
        if (comboResetTime < 0)
        {
            comboCount = 0;
        }
        comboText.text = comboCount.ToString();
    }
    void spawnObj()
    {
        Instantiate(floatObj, floatSpawnPos + Vector2.up * Random.Range(randomYposRange.minimum, randomYposRange.maximum), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
    void spawnJumpObj()
    {
        Instantiate(jumpObj, jumpSpawnPos + Vector2.right * Random.Range(jumpBallspawnPos.minimum, jumpBallspawnPos.maximum), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
    public string GetThousandCommaText(long data)
    {
        return string.Format("{0:#,###}", data);
    }
}
