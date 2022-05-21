using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class jumpColorObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //public Vector3 moveSpeed;
    Rigidbody2D rigid;
    public Vector2 jumpHeight;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(jumpHeight);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
    }

    void Update()
    {
        if (transform.position.y < -4)
            Destroy(gameObject);
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
        float minusColor = (spriteRenderer.color.r + spriteRenderer.color.b) / 2;

        GreenObjectManager.instance.greenGaugeValue += spriteRenderer.color.g - minusColor;
        Destroy(gameObject);
    }
}
