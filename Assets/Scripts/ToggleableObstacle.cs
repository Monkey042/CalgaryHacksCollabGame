using DG.Tweening;
using UnityEngine;

public class ToggleableObstacle : MonoBehaviour
{
    [SerializeField]
    private Vector2 basePosition;

    public float xOffset = 5;
    public float yOffset = 0;
    public float movementDuration = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition.x = Mathf.Floor(transform.position.x);
        basePosition.y = Mathf.Floor(transform.position.y);
    }

    public void ActivateFloor()
    {
        Vector2 offset = new Vector2(xOffset, yOffset);
        transform.DOMove(basePosition + offset, movementDuration);
    }
}
