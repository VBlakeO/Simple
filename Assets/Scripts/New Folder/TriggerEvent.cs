using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [Space]

    public Color[] color;

    Block_Manager block_Manager;

    private void Start()
    {
        block_Manager = Block_Manager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BlockBehaviour>())
        {
            other.GetComponent<BlockBehaviour>().inPosition = true;
            spriteRenderer.color = color[0];
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BlockBehaviour>())
        {
            other.GetComponent<BlockBehaviour>().inPosition = false;
            spriteRenderer.color = color[1];
         
            if (other.gameObject == block_Manager.blocks[0] && other.GetComponent<BlockBehaviour>().canBeThrown)
                block_Manager.RemoveBlock(0);
        }
    }
}
