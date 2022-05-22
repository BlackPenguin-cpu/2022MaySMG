using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class colorObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Vector3 moveSpeed;
    public FloatReference greenValue;
    public GameObject particle;
    public GameObject deadCell;
    public List<Sprite> sprites;
    Rigidbody2D rigid;

    float colorValue;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddTorque(Random.Range(-30, 30));

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
        float minusColor = (spriteRenderer.color.r + spriteRenderer.color.b) / 2;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];

        colorValue += spriteRenderer.color.g - minusColor;
        if (colorValue > 0)
        {
            spriteRenderer.color += new Color(-1, 0, -1);
            particle.SetActive(true);
        }
    }
    private void Update()
    {
        if (transform.position.x < -9)
            Destroy(gameObject);
        if (GreenObjectManager.instance.isClicked)
            ClickCheck();
        Move();
    }
    private void Move()
    {
        transform.position += moveSpeed * Time.deltaTime;

    }
    void ClickCheck()
    {
        RaycastHit2D[] obj = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        foreach (RaycastHit2D hit in obj)
        {
            if (hit.transform.GetComponent<colorObject>() == this)
            {
                OnClicked();
            }
        }
    }
    void OnClicked()
    {
        greenValue.Value += colorValue * 20;
        if (colorValue > 0)
        {
            GreenObjectManager.instance._comboCount++;
        }
        else
        {
            GreenObjectManager.instance._comboCount = 0;
        }
        GameObject obj = Instantiate(deadCell, transform.position, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().color = new Color(0.6509804f, 0.8862745f, 0.3803922f);
        Destroy(gameObject);
    }
}
