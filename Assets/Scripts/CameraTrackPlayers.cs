using DG.Tweening;
using UnityEngine;

public class CameraTrackPlayers : MonoBehaviour
{
    public GameObject player;
    public float offsetRadiusX = 8.5f;
    public float offsetRadiusY = 3f;

    private Vector2 playerPrevPosition;

    // Update is called once per frame
    void Update()
    {
        if (ChangeCurrentMover.Instance.currentMoverGO != player)
        {
            player = ChangeCurrentMover.Instance.currentMoverGO;
            playerPrevPosition = player.transform.position;

            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            transform.DOMove(newPos, 1);
        }

        Vector2 delta_position = (Vector2)player.transform.position - playerPrevPosition;
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
        playerPrevPosition = player.transform.position;
    }
}
