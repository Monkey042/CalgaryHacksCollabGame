using UnityEngine;

public class ForceMovePlayerCollision : MonoBehaviour
{
    public Vector2 playerOffsetAfterCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = (Vector2) collision.gameObject.transform.position + playerOffsetAfterCollision;
        }
    }
}
