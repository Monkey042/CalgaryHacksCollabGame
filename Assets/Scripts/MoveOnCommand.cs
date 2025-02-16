using DG.Tweening;
using UnityEngine;

public class MoveOnCommand : MonoBehaviour
{
    public GameObject connectedButton;
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
        if (connectedButton.GetComponent<ButtonScript>().isButtonPressed == true)
        {
            transform.DOMove(origPos + moveOffsets, animDuration);
        } else
        {
            transform.DOMove(origPos, animDuration);
        }
    }
}
