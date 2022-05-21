using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class colorObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Vector3 moveSpeed;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
    }
    private void Update()
    {
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
        float minusColor = (spriteRenderer.color.r + spriteRenderer.color.b) / 2;

        GreenObjectManager.instance.greenGaugeValue += spriteRenderer.color.g - minusColor;
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        //아마 뭐가 있지 않을까요?
    }
}
