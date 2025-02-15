using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Movement>().moverType == MoverType.Pusher)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
}
