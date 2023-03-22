using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organizer : MonoBehaviour
{
    public Color sessionColor;
    public SpriteRenderer[] spriteRenderer;
    public int order = 0;

    private void OnValidate()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();

        foreach (var sr in spriteRenderer)
        {
            sr.color = sessionColor;
            sr.sortingOrder = order;
        }
    }
}
