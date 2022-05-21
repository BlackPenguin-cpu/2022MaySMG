using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeadCell : MonoBehaviour
{
    public Transform Gauge;
    float speed = 2;
    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, Camera.main.ScreenToWorldPoint(Gauge.position), Time.deltaTime * speed);
        speed += Time.deltaTime * 10;
        if(Vector2.Distance(transform.position, Gauge.position) < 1)
        {
            Destroy(gameObject);
        }
    }
}
