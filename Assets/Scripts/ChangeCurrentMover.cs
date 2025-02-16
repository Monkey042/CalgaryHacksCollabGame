using System.Collections.Generic;
using UnityEngine;

public class ChangeCurrentMover : MonoBehaviour
{
    public static ChangeCurrentMover Instance;

    public List<GameObject> moversList = new List<GameObject>();

    public int currentMover = 0;

    public GameObject currentMoverGO;

    public KeyCode changeKey = KeyCode.C;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(changeKey))
        {
            if(moversList.Count > 0)
            {
                if (currentMover + 1 <= moversList.Count - 1)
                {
                    moversList[currentMover].GetComponent<Movement>().isActive = false;
                    currentMover++;
                    moversList[currentMover].GetComponent<Movement>().isActive = true;
                    FindFirstObjectByType<DuplicationUI>().player = moversList[currentMover];
                }
                else
                {
                    moversList[currentMover].GetComponent<Movement>().isActive = false;
                    currentMover = 0;
                    moversList[currentMover].GetComponent<Movement>().isActive = true;
                }
            }
        }
        currentMoverGO = moversList[currentMover];
    }
}
