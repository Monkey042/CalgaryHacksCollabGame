using DG.Tweening;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    private Vector2 basePosition;
    [SerializeField]
    private bool movingAway = true;

    public Vector2 offset;
    public float movementDuration = 2;

    private float distMoved;
    private float distGoal;

    public bool toggled = false;
    public GameObject button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distGoal = (offset.x + basePosition.x) + (offset.y + basePosition.y);
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (button == null || button.GetComponent<ButtonScript>().isButtonPressed)
        {
            toggled = true;
        }
        else
        {
            toggled = false;
        }

        if (toggled)
        {
            distMoved = (transform.position.x - basePosition.x) + (transform.position.y - basePosition.y);

            if (distMoved >= distGoal - 0.01)
            {
                movingAway = false;
            } 
            else if (distMoved < 0.01)
            {
                movingAway = true;
            }

            if (movingAway)
            {
                transform.DOMove(basePosition + offset, movementDuration);
            }
            else
            {
                transform.DOMove(basePosition, movementDuration);
            }
        }

    }
}
