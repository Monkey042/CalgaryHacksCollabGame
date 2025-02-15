using DG.Tweening;
using UnityEngine; 

public enum MoverType { Basic, Jumper, Slower, Shrinker, Pusher }

public class Movement : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    public MoverType moverType;

    [Header("Jumper Settings")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;

    [Header("Shrinker Settings")]
    private bool isShrunk;
    public KeyCode shrinkKey = KeyCode.S;
    public float changeSize;
    private float initialSize;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialSize = transform.localScale.y;
    }

    private void Update()
    {
        if (isActive)
        {
            if (moverType == MoverType.Jumper)
            {
                Jumper();
            }
            else if (moverType == MoverType.Shrinker)
            {
                Shrinker();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            float horizontalMove = Input.GetAxis("Horizontal") * speed * 10;
            rb.linearVelocity = new Vector2(horizontalMove, rb.linearVelocity.y);
        }

    }

    private void Jumper()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddRelativeForceY(jumpForce * 10, ForceMode2D.Impulse);
        }
    }

    private void Shrinker()
    {
        if (Input.GetKeyDown(shrinkKey))
        {
            isShrunk = true;
            this.transform.DOScaleY(changeSize, 0.4f);
        }
        else if (Input.GetKeyUp(shrinkKey))
        {
            isShrunk = false;
            this.transform.DOScaleY(initialSize, 0.4f);
        }
    }
}
