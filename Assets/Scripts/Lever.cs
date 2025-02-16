using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public List<MovingFloor> FloorList;

    private bool activated = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!activated)
        {
            foreach (MovingFloor mv in FloorList)
            {
                mv.ActivateFloor();
            }
            transform.localScale.Set(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            activated = true;
        }
    }
}
