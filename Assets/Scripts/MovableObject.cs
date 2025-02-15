using UnityEngine;

public class MovableObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.GetComponent<Movement>().moverType == MoverType.Pusher)
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            } 
            else if (other.gameObject.GetComponent<Movement>().moverType != MoverType.Pusher)
            { 
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }
}
