using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBehavior : MonoBehaviour
{
    public Vector3 rotation;
    public Color[] color;
    public SpriteRenderer[] spriteRenderers;

    public float repeatingSpeed = 1.0f;
    public bool parentComponent;
    int id;

    public void Start()
    {
        if (parentComponent)
            InvokeRepeating("ChangeColor", repeatingSpeed, repeatingSpeed);
    }

    void FixedUpdate()
    {
        transform.Rotate(rotation);
    }

    void ChangeColor()
    {
        if(id < color.Length-1)
        {
            id++;
            foreach (var renderer in spriteRenderers)
            {
                renderer.color = color[id];
            }
        }
        else
        {
            id = 0; ;
            foreach (var renderer in spriteRenderers)
            {
                renderer.color = color[id];
            }
        }
    }
}
