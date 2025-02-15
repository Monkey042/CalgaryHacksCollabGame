using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DuplicationOrb : MonoBehaviour
{
    public DuplicationUI duplicationUI;

    private void OnCollisionEnter2D(Collision2D other)
    {
        duplicationUI.enableCanvas();
        Destroy(gameObject);
        other.gameObject.GetComponent<Movement>().isActive = false;
    }
}
