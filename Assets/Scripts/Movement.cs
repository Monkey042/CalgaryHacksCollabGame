using UnityEngine; 

public enum MoverType { Basic, Jumper, Slower, Shrinker, Pusher }

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    public MoverType moverType;

    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (moverType == MoverType.Jumper)
        {
            Jumper();
        }
    }

    private void FixedUpdate()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * speed * 10;
        rb.linearVelocity = new Vector2(horizontalMove, rb.linearVelocity.y);
    }

    private void Jumper()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddRelativeForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
}
