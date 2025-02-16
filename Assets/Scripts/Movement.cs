using DG.Tweening;
using System.Collections;
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
    public float jumperGroundRadius;
    public LayerMask groundLayer;
    public float jumperCoyoteTime;
    private bool canJump;
    private bool inCoyoteTime, canCoyoteTime;

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

    private void Start()
    {
        if (!ChangeCurrentMover.Instance.moversList.Contains(this.gameObject))
        {
            ChangeCurrentMover.Instance.moversList.Add(this.gameObject);
        }
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
        print(canJump);

        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            rb.AddRelativeForceY(jumpForce * 10, ForceMode2D.Impulse);
            canCoyoteTime = false;
        }

        Vector2 circleCastPos = new Vector2(transform.position.x, transform.position.y - 1.25f);

        RaycastHit2D hit = Physics2D.CircleCast(circleCastPos, jumperGroundRadius, Vector2.down, 0f, groundLayer);
        if(hit)
        {
            canJump = true;
            canCoyoteTime = true;
        }
        else
        {
            if(!inCoyoteTime && canCoyoteTime)
            {
                StartCoroutine(CoyoteTime());
            }
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

    IEnumerator CoyoteTime()
    {
        inCoyoteTime = true;
        yield return new WaitForSeconds(jumperCoyoteTime);
        canJump = false;
        inCoyoteTime = false;
    }
}
