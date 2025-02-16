using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    public int nextLevel;
    private bool inradius;
    void OnTriggerEnter2D(Collider2D collision)
    {
        inradius = true;
        collision.GetComponent<Movement>().headTest.text = "Press: E";
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        inradius = false;
        collision.GetComponent<Movement>().headTest.text = "";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && inradius == true) {
            //loadNextLevel(nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
    }
}
