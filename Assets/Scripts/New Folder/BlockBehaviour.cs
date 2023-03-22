using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{
    public Type type;
    [Space]

    public Rigidbody2D rb = null;

    public bool scored = false;
    public bool inPosition = false;
    public bool canBeThrown = true;

    private float currentSpeed = 100.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        scored = false;
        canBeThrown = true;
    }

    private void FixedUpdate()
    {
        if (canBeThrown)
            rb.velocity = -transform.up * currentSpeed * Time.deltaTime;
    }

    public void SetType(int _type)
    {
        type = (Type) _type;
    }

    public void SetColor(Color[] colors)
    {
        GetComponent<SpriteRenderer>().color = colors[(int) type];
    }

    public void SetCurrentSpeed(float _speed)
    {
        currentSpeed = _speed;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
