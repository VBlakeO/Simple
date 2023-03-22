using UnityEngine;

public class GameController : MonoBehaviour
{
    public BlockManager2 _blockManager;
    GameplayData _gameplayData;
    BlockBehaviour2 _currentBlock;
    public GameObject circularEffect;

    private void Awake()
    {
        _blockManager = GetComponent<BlockManager2>();
        _gameplayData = GetComponent<GameplayData>();
    }

    private void Update()
    {
        if (_blockManager.Blocks.Count == 0)
            return;

        if(_blockManager.Blocks[0])
            _currentBlock = _blockManager.Blocks[0].GetComponent<BlockBehaviour2>();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ThrowToTheLeft();

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ThrowToTheRight();
    }

    public void ThrowToTheRight()
    {
        if (_currentBlock == null)
            return;

        if (_currentBlock.canBeThrown && _currentBlock.InPosition)
        {
            _currentBlock.possessed = true;
            _currentBlock.Rb.velocity = Vector2.zero;
            _currentBlock.Rb.AddForce(_gameplayData.launchForce, ForceMode2D.Impulse);
            _currentBlock.canBeThrown = false;

            UseCircularEffect(_currentBlock.GetComponent<SpriteRenderer>().color);

            if (_blockManager && _blockManager.Blocks.Count > 0)
                _blockManager.Blocks.RemoveAt(0);
        }

        print("ToTheRight");
    }

    public void ThrowToTheLeft()
    {
        if (_currentBlock == null)
            return;

        if (_currentBlock.canBeThrown && _currentBlock.InPosition)
        {
            _currentBlock.possessed = true;
            _currentBlock.Rb.velocity = Vector2.zero;
            _currentBlock.Rb.AddForce(new Vector2(-_gameplayData.launchForce.x, _gameplayData.launchForce.y), ForceMode2D.Impulse);
            _currentBlock.canBeThrown = false;

            UseCircularEffect(_currentBlock.GetComponent<SpriteRenderer>().color);

            if (_blockManager && _blockManager.Blocks.Count > 0)
                _blockManager.Blocks.RemoveAt(0);
        }

        print("ToTheLeft");
    }

    void UseCircularEffect(Color _color)
    {
        circularEffect.GetComponent<Animator>().Play("CircleEffect", 0, 0);
        circularEffect.GetComponent<SpriteRenderer>().color = _color;
    }
}
