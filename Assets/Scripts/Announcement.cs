using System.Collections;
using UnityEngine;

public class Announcement : MonoBehaviour
{
    public AnnouncementBlock[] announcementBlocks;
    [Space]

    public Color defeatColor;
    [Space]
    
    public float effectTime;

    private void Start()
    {
        GameManager.Instance.defeatDelegate += Organize;
    }

    public void Organize()
    {
        StartCoroutine(ActiveEffect());
    }

    public IEnumerator ActiveEffect()
    {
        WaitForSeconds wfs = new WaitForSeconds(effectTime);

        for (int i = announcementBlocks.Length - 1; i >= 0; i--)
        {
            for (int j = 0; j < announcementBlocks[i].m_blocks.Length; j++)
            {
                announcementBlocks[i].m_blocks[j].GetComponent<SpriteRenderer>().color = defeatColor;
                announcementBlocks[i].m_blocks[j].GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
            yield return wfs;
        }
    }
}

[System.Serializable]
public class AnnouncementBlock
{ 
    public Transform[] m_blocks;
}