using DG.Tweening;
using UnityEngine;

public class ToggleableObstacle : MonoBehaviour
{
    public Vector2 offset;
    public float movementDuration = 2;

    public void ActivateFloor()
    {
        transform.DOMove((Vector2) transform.position + offset, movementDuration);
    }
}
