using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public float buttonMoveOffset;
    public bool isButtonPressed;

    private float posYInit;

    [SerializeField]
    private List<GameObject> currentlyColliding;
    private void Start()
    {
        posYInit = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentlyColliding.Count == 0)
        {
            isButtonPressed = true;
            transform.DOMoveY(posYInit + buttonMoveOffset, 0.5f);
        }

        currentlyColliding.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentlyColliding.Remove(collision.gameObject);

        if (currentlyColliding.Count <= 0)
        {
            isButtonPressed = false;
            transform.DOMoveY(posYInit, 0.5f);
        }
    }

    private void Update()
    {
      
    }
}
