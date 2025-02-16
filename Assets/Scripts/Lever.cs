using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public List<ToggleableObstacle> toggleableObstacles;

    private bool activated = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activated)
        {
            foreach (ToggleableObstacle to in toggleableObstacles)
            {
                to.ActivateFloor();
            }
            Vector2 newScale = transform.localScale;
            newScale.y *= -1f;
            transform.localScale = newScale;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            activated = true;
        }
    }
}
