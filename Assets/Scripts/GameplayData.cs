using UnityEngine;

public class GameplayData : MonoBehaviour
{
    static public GameplayData Instance;

    [Header("Block")]
    public float initialSpeed = 200.0f;
    public float speedModifier = 10.0f;
    public float spawnInterval = 2.0f;
    public float intervalMultiplier = 1.0f;

    [Header("Player")]
    public Vector2 launchForce = new Vector2(10.0f, 2.0f);

    private void Awake()
    {
        Instance = this;
    }
}