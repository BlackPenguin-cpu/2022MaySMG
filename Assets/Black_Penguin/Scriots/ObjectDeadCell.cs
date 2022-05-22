using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class ObjectDeadCell : MonoBehaviour
{
    public Transform Gauge;

    public GameObjectReference guageBar;

    float speed = 2;
    private void Start()
    {
        Gauge = GameObject.Find("DeadCellPoint").transform;
    }
    private void Update()
    {
        
        transform.position = Vector3.Slerp(transform.position, Camera.main.ScreenToWorldPoint(Gauge.position), Time.deltaTime * speed);
        speed += Time.deltaTime * 10;
        if(Vector2.Distance(transform.position, Gauge.position) < 1)
        {
            Destroy(gameObject);
        }
        
        /*
        transform.position = Vector3.Slerp(transform.position, Camera.main.ScreenToWorldPoint(guageBar.Value.transform.position), Time.deltaTime * speed);
        speed += Time.deltaTime * 10;
        if (Vector2.Distance(transform.position, guageBar.Value.transform.position) < 1)
        {
            Destroy(gameObject);
        }
        */
    }
}
