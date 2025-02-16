using DG.Tweening;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    [SerializeField]
    private Vector2 basePosition;

    public float xOffset = 5;
    public float movementDuration = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition.x = Mathf.Floor(transform.position.x);
        basePosition.y = Mathf.Floor(transform.position.y);
    }

    public void ActivateFloor()
    {
        transform.DOMoveX(basePosition.x + xOffset, movementDuration);
    }
}
