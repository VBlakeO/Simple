using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Manager : MonoBehaviour
{
    static public Block_Manager Instance { get; private set; }

    public Transform spawnPoint = null;
    public GameObject blockBehaviourPrefab = null;
    [Space]

    public int waveIndex = 0;
    public float intervalBetweenWaves = 10.0f;
    [Space]

    public List<GameObject> blocks;
    public List<GameObject> generatedBlocks;
    public List<GameObject> allBlocks;
    public WaveBehaviour[] waveBehaviours = null;

    private bool defeat = false;

    private PlayerController _playerController;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.defeatDelegate += Defeat;

        _playerController = PlayerController.Instance;

        for (int i = 0; i < generatedBlocks.Count; i++)
        {
            allBlocks.Add(generatedBlocks[i]);
        }

        StartCoroutine(ChangeWave());
        StartCoroutine(SpawnBlocks());
    }

    public void Defeat()
    {
        defeat = true;

        StopCoroutine(SpawnBlocks());

        foreach (GameObject block in blocks)
        {
            block.SetActive(false);
            block.GetComponent<Rigidbody2D>().simulated = false;
        }      
        
        foreach (GameObject block in generatedBlocks)
        {
            block.SetActive(false);
            block.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    public void RemoveBlock(int index)
    {
        generatedBlocks.Add(blocks[index]);
        blocks.RemoveAt(index);
    }
    
    private IEnumerator ChangeWave()
    {
        WaitForSeconds wfs = new WaitForSeconds(intervalBetweenWaves);
        
        yield return wfs;

        int currentWave = waveIndex;
       
        while (waveIndex == currentWave)
        {
            waveIndex = Random.Range(0, waveBehaviours.Length);
            yield return null;
        }

        StartCoroutine(ChangeWave());
    }

    private IEnumerator SpawnBlocks()
    {
        WaveBehaviour _waveBehaviour = waveBehaviours[waveIndex];
        WaitForSeconds wfs = new WaitForSeconds(_waveBehaviour.distanceVariation[Random.Range(0, _waveBehaviour.distanceVariation.Length)]);

        yield return wfs;

        if(!defeat)
        {
            GameObject _block = generatedBlocks[0];
            BlockBehaviour _blockBehaviour = _block.GetComponent<BlockBehaviour>();
            
            _block.transform.position = spawnPoint.position;
            _block.transform.rotation = Quaternion.identity;
            _block.gameObject.SetActive(true);
          
            if (_blockBehaviour)
            {
                _blockBehaviour.SetType(Random.Range(0, 2));
                _blockBehaviour.SetColor(_waveBehaviour.colors);
                _blockBehaviour.SetCurrentSpeed(_waveBehaviour.blockSpeed[Random.Range(0, _waveBehaviour.blockSpeed.Length)]);
            }

            blocks.Add(_block);
            generatedBlocks.RemoveAt(0);

            StartCoroutine(SpawnBlocks());
        }
    }

    public void ChangeColor()
    {
        if (blocks.Count == 0)
            return;

        SpriteRenderer sprite = blocks[0].GetComponent<SpriteRenderer>();

        if (sprite.color == waveBehaviours[waveIndex].colors[0] || sprite.color == waveBehaviours[waveIndex].colors[1])
        {
            Hud_Manager.Instance.UpdateEdgesColor(waveBehaviours[waveIndex].colors);
            print("AIda");
        }
    }
}

[System.Serializable]
public class WaveBehaviour
{
    public float[] blockSpeed;

    public Color[] colors = null;

    public float[] distanceVariation;
}
