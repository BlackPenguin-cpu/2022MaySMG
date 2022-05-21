using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class jumpColorObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //public Vector3 moveSpeed;
    Rigidbody2D rigid;
    public Vector2 jumpHeight;
    public GameObject particle;
    public GameObject deadCell;
    public FloatReference greenValue;

    float colorValue;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(Random.Range(0, 2) == 0 ? jumpHeight.x : -jumpHeight.x, jumpHeight.y), ForceMode2D.Impulse);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();

        float minusColor = (spriteRenderer.color.r + spriteRenderer.color.b) / 2;

        colorValue += spriteRenderer.color.g - minusColor;
        if (colorValue > 0)
        {
            particle.SetActive(true);
        }
    }

    void Update()
    {
        if (transform.position.y < -6)
            Destroy(gameObject);
        if (GreenObjectManager.instance.isClicked)
            ClickCheck();
    }
    void ClickCheck()
    {
        RaycastHit2D[] obj = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        foreach (RaycastHit2D hit in obj)
        {
            if (hit.transform.GetComponent<jumpColorObject>() == this)
            {
                OnClicked();
            }
        }
    }
    void OnClicked()
    {
        greenValue.Value += colorValue;
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
