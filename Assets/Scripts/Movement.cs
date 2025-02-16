using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine; 
using System.Collections.Generic;
using System.Linq;
using System;
using TMPro;

public enum MoverType { Basic, Jumper, Slower, Shrinker, Pusher }

public class Movement : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private float speed = 1.5f;
    private float trueSpeed;
    [SerializeField] private float slowFactor = 4;
    private float fixedDeltaTime;
    private Rigidbody2D rb;

    [SerializeField] SpriteRenderer spitrend;

    public MoverType moverType;

    public TMP_Text headTest;

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

    public KeyCode pickupKey = KeyCode.F;
    public Transform holdPos;
    private Transform originalHoldPos;
    private bool isPickedUp;
    private Transform pickerUpperHoldPos;
    [SerializeField] private GameObject heldImmediate;
    public List<GameObject> held = new List<GameObject>();

    public KeyCode throwKey = KeyCode.R;
    public float throwForce;
    private Vector2 throwDir;
    public float upwardsForce = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialSize = transform.localScale.y;
        this.fixedDeltaTime = Time.fixedDeltaTime;
        holdPos = transform.GetChild(0).GetComponent<Transform>();
        originalHoldPos = transform.GetChild(0).GetComponent<Transform>();
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

            if (Input.GetKeyDown(pickupKey))
            {
                Pickup();
            }

            if (Input.GetKeyDown(throwKey))
            {
                Throw();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                throwDir = new Vector2(-1, upwardsForce).normalized;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                throwDir = new Vector2(1, upwardsForce).normalized;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                throwForce *= 10;
            }
        }

        if (isPickedUp)
        {
            transform.position = pickerUpperHoldPos.position;
        }

        if (isActive)
        {
            float horizontalMove = Input.GetAxis("Horizontal") * trueSpeed * 10;
            if(horizontalMove > 0)
            {
                spitrend.flipX = true;
            }
            else if(horizontalMove < 0)
            {
                spitrend.flipX = false;
            }
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
        canJump = false;
        yield return new WaitForSeconds(jumperCoyoteTime);
        inCoyoteTime = false;
    }

    public void Pickup()
    {
        GameObject pickedUp = GetClosestMover();
        
        if (pickedUp != null)
        {
            Movement pickedUpMovement = pickedUp.GetComponent<Movement>();
            pickedUpMovement.pickerUpperHoldPos = holdPos;
            pickedUpMovement.isPickedUp = true;
            holdPos = pickedUp.transform.GetChild(0).GetComponent<Transform>();
            held.Add(pickedUp);
            foreach (GameObject go in pickedUpMovement.held)
            {
                held.Add(go);
            }
            heldImmediate = held[0];
            for (int i = 0; i < held.Count; i++)
            {
                Movement heldIMovement = held[i].GetComponent<Movement>();
                heldIMovement.held = new List<GameObject>();
                for (int j = i + 1; j < held.Count; j++)
                {
                    heldIMovement.held.Add(held[j]);
                }
                
                if (heldIMovement.held.Count > 0)
                {
                    heldIMovement.heldImmediate = heldIMovement.held[0];
                }

                heldIMovement.holdPos = held[held.Count - 1].GetComponent<Movement>().holdPos;
            }
        }
    }

    public GameObject GetClosestMover()
    {
        GameObject goMin = null;
        float minDist = 1;
        Vector3 currentPos = transform.position;
        foreach (GameObject go in ChangeCurrentMover.Instance.moversList)
        {
            float dist = Vector3.Distance(go.transform.position, currentPos);
            if (dist < minDist && go != gameObject && go.GetComponent<Movement>().isPickedUp == false)
            {
                goMin = go;
                minDist = dist;
            }
        }
        return goMin;
    }

    public void Throw()
    {
        if (heldImmediate != null && isPickedUp == false)
        { 
            heldImmediate.GetComponent<Movement>().isPickedUp = false;
            heldImmediate.GetComponent<Movement>().pickerUpperHoldPos = null;
            heldImmediate.GetComponent<Rigidbody2D>().AddForce(throwDir * throwForce * 100, ForceMode2D.Force);
            holdPos = originalHoldPos;
            heldImmediate = null;
            held = new List<GameObject>();
        }
    }
}
