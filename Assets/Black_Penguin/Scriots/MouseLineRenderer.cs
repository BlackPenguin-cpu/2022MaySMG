using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLineRenderer : MonoBehaviour
{
    public GameObject Particle;
    private void Update()
    {
        if (Particle.activeSelf != GreenObjectManager.instance.isClicked)
            Particle.SetActive(GreenObjectManager.instance.isClicked);
        Particle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
    }
}
