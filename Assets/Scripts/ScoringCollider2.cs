using UnityEngine;

public enum Type { LEFT, RIGHT }//, DEATH_LINE }

public class ScoringCollider2 : MonoBehaviour
{
    public Type type;
    [Space]

    public int scoring = 0;
    public GameManager2 gameManager;
    public BlockManager2 _blockManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BlockBehaviour2>())
        {
            BlockBehaviour2 _block = collision.GetComponent<BlockBehaviour2>();

            if (!_block.scored)
            {
                if(_block.type == type)
                    gameManager.AddScore(scoring);
                else
                {
                    gameManager.AddScore(-scoring);
                    gameManager.RemoveChance();
                }

                //if (type == Type.DEATH_LINE)
                //{
                //    if (_block.canBeThrown)
                //    {
                //        if (_blockManager && _blockManager.Blocks.Count > 0)
                //            _blockManager.Blocks.RemoveAt(0);
                //    }
                //}


                _block.scored = true;
                Destroy(collision.gameObject);
            }
        }
    }
}
