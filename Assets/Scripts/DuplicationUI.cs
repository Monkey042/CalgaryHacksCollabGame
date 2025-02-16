using UnityEngine;

public class DuplicationUI : MonoBehaviour
{
    public Canvas canvas;

    public GameObject player;

    public float yOffset = 1f, xOffset = 1f;

    public void enableCanvas() 
    {
        Time.timeScale = 0f;
        canvas.enabled = true;
    }

    public void disableCanvas()
    {
        Time.timeScale = 1f;
        canvas.enabled = false;
    }

    public void duplicate(GameObject playerPrefab)
    {
        disableCanvas();
        Vector3 offset = new Vector3(xOffset, yOffset, 0);
        GameObject duplicate = Instantiate(playerPrefab, player.transform.position + offset, Quaternion.Euler(0, 0, 0));
        duplicate.GetComponent<Movement>().isActive = true;
        ChangeCurrentMover.Instance.moversList.Add(duplicate);
        ChangeCurrentMover.Instance.currentMover = ChangeCurrentMover.Instance.moversList.IndexOf(duplicate);
    }
}
