using UnityEngine; 

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed * 10;

        print(horizontalMove);

        rb.linearVelocity = new Vector2(horizontalMove, rb.linearVelocity.y);
    }
}
