using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager2 : MonoBehaviour
{
    static public BlockManager2 Instance { get; private set; }

    public GameObject block;
    public Transform spawnPoint;
    GameplayData _gameplayData;

    float _currentSpeed = 0.0f;

    bool defeat = false;
    public float spawnSpeed = 0.7f;

    public List<GameObject> Blocks;

    void Start()
    {
        Instance = this;

        _gameplayData = GetComponent<GameplayData>();

        _currentSpeed = _gameplayData.initialSpeed;

        StartCoroutine(SpawnBlocks());
    }

    public void RemoveFirstBlock()
    {
        Blocks.RemoveAt(0);
    }

    public void Defeat()
    {
        defeat = true;
        StopCoroutine(SpawnBlocks());

        foreach (GameObject block in Blocks)
        {
            block.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            block.GetComponent<BlockBehaviour2>().canBeThrown = false;
            block.SetActive(false);
        }
    }

    IEnumerator SpawnBlocks()
    {
        WaitForSeconds wfs = new WaitForSeconds(_gameplayData.spawnInterval * _gameplayData.intervalMultiplier);

        yield return wfs;

        if (!defeat)
        {
            GameObject _block = Instantiate(block, spawnPoint.position, Quaternion.identity);
            if (_block.GetComponent<BlockBehaviour2>())
            {
                _block.GetComponent<BlockBehaviour2>().SetType(Random.Range(0, 2));
                _block.GetComponent<BlockBehaviour2>().SetCurrentSpeed(_currentSpeed);
            }
            Blocks.Add(_block);
            StartCoroutine(SpawnBlocks());
        }
    }
}
