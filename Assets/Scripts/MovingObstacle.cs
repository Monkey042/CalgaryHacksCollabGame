using DG.Tweening;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    private Vector2 basePosition;
    [SerializeField]
    private bool isMovingUp = false;
    [SerializeField]
    private bool atBase = true;

    public float yOffsetUp = 5;
    public float movementDuration = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition.x = Mathf.Floor(transform.position.x);
        basePosition.y = Mathf.Floor(transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Floor(transform.position.y) <= basePosition.y)
        {
            atBase = true;
        } else
        {
            atBase = false;
        }

        if (transform.position.y < basePosition.y + yOffsetUp && !isMovingUp && atBase)
        {
            isMovingUp = true;
            transform.DOMoveY(basePosition.y + yOffsetUp, movementDuration);
        }
        else if (transform.position.y >= basePosition.y + yOffsetUp && isMovingUp && !atBase)
        {
            isMovingUp = false;
            transform.DOMoveY(basePosition.y, movementDuration);
        }
    }
}
