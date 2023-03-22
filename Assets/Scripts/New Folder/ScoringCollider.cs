using UnityEngine;

public class ScoringCollider : MonoBehaviour
{
    public Type type;
    [Space]

    public int scoring = 0;
    public bool deathTrigger = false;

    private GameManager _gameManager = null; 
    private Block_Manager _blockManager = null;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _blockManager = Block_Manager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BlockBehaviour>())
        {
            BlockBehaviour block = collision.GetComponent<BlockBehaviour>();

            if (deathTrigger)
            {
                if (!block.scored)
                    _gameManager.UpdateScore(-scoring);
            }
            else
            {
                if (!block.scored)
                {
                    if (block.type == type)
                        _gameManager.UpdateScore(scoring);
                    else
                        _gameManager.UpdateScore(-scoring);
                }
            }

            block.scored = true;
            block.Disable();
        }
    }
}
