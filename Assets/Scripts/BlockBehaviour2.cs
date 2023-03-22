using UnityEngine;

public class BlockBehaviour2 : MonoBehaviour
{
    public Type type;
    [Space]

    public bool possessed = false;
    public bool canBeThrown = true;
    public bool InPosition = false;
    public bool scored = false;

    public Color color_0 = Color.white;
    public Color color_1 = Color.white;

    float _currentSpeed = 0.0f;

    Rigidbody2D rb;
    public Rigidbody2D Rb { get => rb;}

    private void Awake()
    {
        SetCurrentSpeed(100);
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetType(int _type)
    {
        if(_type == 0)
        {
            type = Type.LEFT;
            GetComponent<SpriteRenderer>().color = color_0;
        }
        else
        {
            type = Type.RIGHT;
            GetComponent<SpriteRenderer>().color = color_1;
        }
    }

    public void SetCurrentSpeed(float _speed)
    {
        _currentSpeed = _speed;
    }

    private void FixedUpdate()
    {
        if (canBeThrown)
            Rb.velocity = -transform.up * _currentSpeed * Time.deltaTime;
    }
}
