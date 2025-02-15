using UnityEngine;

public class CameraTrackPlayers : MonoBehaviour
{
    public GameObject player;
    public float offsetRadiusX = 8.5f;
    public float offsetRadiusY = 4f;
    
    private Vector2 player_prev_position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta_position = (Vector2)player.transform.position - player_prev_position;
        Vector3 newPosition = transform.position;

        // Adjust X
        if (Mathf.Abs(player.transform.position.x - transform.position.x) > offsetRadiusX)
        {
            newPosition.x += delta_position.x;
        }

        // Adjust Y
        if (Mathf.Abs(player.transform.position.y - transform.position.y) > offsetRadiusY)
        {
            newPosition.y += delta_position.y;
        }

        transform.position = newPosition;
        player_prev_position = player.transform.position;
    }
}
