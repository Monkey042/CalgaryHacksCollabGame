using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerUIScript : MonoBehaviour
{
    public Image activePlayerImg; // This is the UI Image component
    public TMP_Text activePlayerText;

    public Sprite defaultSprite; // Change from Image to Sprite
    public string defaultText;
    public List<Sprite> activePlayerSprites; // Change from List<Image> to List<Sprite>

    private int currentActive = -1;
    public Dictionary<MoverType, int> activeMoverTypeToInt = new Dictionary<MoverType, int>();
    private MoverType curMoverType;

    void Start()
    {
        activePlayerImg.sprite = defaultSprite; // Assign the sprite instead of replacing the Image component
        activePlayerText.text = defaultText;

        activeMoverTypeToInt.Add(MoverType.Basic, 0);
        activeMoverTypeToInt.Add(MoverType.Jumper, 1);
        activeMoverTypeToInt.Add(MoverType.Slower, 2);
        activeMoverTypeToInt.Add(MoverType.Pusher, 3);
        activeMoverTypeToInt.Add(MoverType.Shrinker, 4);
    }

    void Update()
    {
        if (currentActive == -1)
        {
            curMoverType = ChangeCurrentMover.Instance.currentMoverGO.GetComponent<Movement>().moverType;
            currentActive = activeMoverTypeToInt[curMoverType];
            activePlayerImg.sprite = activePlayerSprites[currentActive]; // Assign sprite instead of Image
            activePlayerText.text = curMoverType.ToString();
        }
        else if (activeMoverTypeToInt[ChangeCurrentMover.Instance.currentMoverGO.GetComponent<Movement>().moverType] != currentActive)
        {
            curMoverType = ChangeCurrentMover.Instance.currentMoverGO.GetComponent<Movement>().moverType;
            currentActive = activeMoverTypeToInt[curMoverType];
            activePlayerImg.sprite = activePlayerSprites[currentActive]; // Assign sprite instead of Image
            activePlayerText.text = curMoverType.ToString();
        }
    }
}
