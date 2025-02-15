using UnityEngine;

public class CameraTrackPlayers : MonoBehaviour
{
    public GameObject player;
    public float offsetRadius = 8.5f;
    
    private Vector2 player_prev_position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > offsetRadius)
        {
            Vector2 delta_position;
            delta_position = (Vector2)player.transform.position - player_prev_position;
            transform.Translate(delta_position);
        }
        player_prev_position = player.transform.position;
    }
}
