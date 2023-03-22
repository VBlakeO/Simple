using UnityEngine;

public class TriggerEvent2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [Space]
    public Color initialColor;
    public Color activeColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BlockBehaviour2>())
        {
            other.GetComponent<BlockBehaviour2>().InPosition = true;
            spriteRenderer.color = activeColor;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BlockBehaviour2>())
        {
            other.GetComponent<BlockBehaviour2>().InPosition = false;
            spriteRenderer.color = initialColor;
        }
    }

}
