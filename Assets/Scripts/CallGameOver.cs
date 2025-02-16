using UnityEngine;

public class CallGameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameOverCanvas.GetComponent<GameOverMenu>().GameOver();
    }
}
