using DG.Tweening;
using UnityEngine; 

public enum MoverType { Basic, Jumper, Slower, Shrinker, Pusher }

public class Movement : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private float speed = 1.5f;
    private float trueSpeed;
    [SerializeField] private float slowFactor = 4;
    private float fixedDeltaTime;
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
        this.fixedDeltaTime = Time.fixedDeltaTime;
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
            Time.timeScale = 1 / slowFactor;
            trueSpeed = speed * slowFactor;

            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            float horizontalMove = Input.GetAxis("Horizontal") * trueSpeed * 10;
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
