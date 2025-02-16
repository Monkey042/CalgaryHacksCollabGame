using UnityEngine;

public class DuplicationUI : MonoBehaviour
{
    public Canvas canvas;

    public GameObject player;

    public float yOffset = 1f, xOffset = 1f;

    private void Update()
    {
        player = ChangeCurrentMover.Instance.currentMoverGO;
    }

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
        bool justTeleport = false;

        disableCanvas();
        Vector3 offset = new Vector3(xOffset, yOffset, 0);

        foreach(GameObject obj in ChangeCurrentMover.Instance.moversList)
        {
            if(obj.GetComponent<Movement>().moverType == playerPrefab.GetComponent<Movement>().moverType)
            {
                print("Same");
                justTeleport = true;
                obj.transform.position = player.transform.position + offset;
                obj.GetComponent<Movement>().isActive = true;
                ChangeCurrentMover.Instance.currentMover = ChangeCurrentMover.Instance.moversList.IndexOf(obj);
            }
            else
            {
                justTeleport = false;
                print("Different");
                print(obj.GetComponent<Movement>().moverType);
                print(obj.GetComponent<Movement>().moverType);
            }
        }
        if (justTeleport == false)
        {
            GameObject duplicate = Instantiate(playerPrefab, player.transform.position + offset, Quaternion.Euler(0, 0, 0));
            duplicate.GetComponent<Movement>().isActive = true;
            ChangeCurrentMover.Instance.moversList.Add(duplicate);
            ChangeCurrentMover.Instance.currentMover = ChangeCurrentMover.Instance.moversList.IndexOf(duplicate);
        }
    }
}
