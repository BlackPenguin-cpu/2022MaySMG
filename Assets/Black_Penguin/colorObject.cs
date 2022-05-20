using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Random.ColorHSV();
    }
    void Update()
    {
    }
    void OnClicked()
    {
        float minusColor = (spriteRenderer.color.r + spriteRenderer.color.b) / 2;
    }
}
