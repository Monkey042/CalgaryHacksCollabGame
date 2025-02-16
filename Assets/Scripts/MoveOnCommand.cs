using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCommand : MonoBehaviour
{
    public List<GameObject> connectedButtons = new List<GameObject>();
    public Vector2 moveOffsets;
    public float animDuration = 1f;

    private Vector2 origPos;

    private void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (allButtonsPressed())
        {
            transform.DOMove(origPos + moveOffsets, animDuration);
        } else
        {
            transform.DOMove(origPos, animDuration);
        }
    }

    public bool allButtonsPressed()
    {
        foreach (GameObject go in connectedButtons)
        {
            if (!go.GetComponent<ButtonScript>().isButtonPressed)
            {
                return false;
            }
        }
        return true;
    }
}
