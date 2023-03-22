using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance { get; private set; }

    public Vector2 launchForce;

    private Block_Manager _blockManager;
    private BlockBehaviour _blockBehaviour;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _blockManager = Block_Manager.Instance;
    }

    private void Update()
    {
        if (_blockManager.blocks.Count == 0)
            return;

        if (_blockManager.blocks[0])
            _blockBehaviour = _blockManager.blocks[0].GetComponent<BlockBehaviour>();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ThrowToTheLeft();

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ThrowToTheRight();

    }
   
    bool PrepareLaunch()
    {
        bool result = false;

        if (_blockBehaviour == null)
            result = false;

        if (_blockBehaviour.canBeThrown && _blockBehaviour.inPosition)
        {
            _blockBehaviour.rb.velocity = Vector2.zero;
            _blockBehaviour.canBeThrown = false;
            _blockManager.RemoveBlock(0);
            result = true;
        }

        return result;
    }

    public void ThrowToTheLeft()
    {
        if (_blockBehaviour == null)
            return;

        if (PrepareLaunch())
        {
            _blockBehaviour.rb.AddForce(new Vector2(-launchForce.x, launchForce.y), ForceMode2D.Impulse);
        }
    }

    public void ThrowToTheRight()
    {
        if (_blockBehaviour == null)
            return;

        if (PrepareLaunch())
        {
            _blockBehaviour.rb.AddForce(new Vector2(launchForce.x, launchForce.y), ForceMode2D.Impulse);
        }
    }
}
