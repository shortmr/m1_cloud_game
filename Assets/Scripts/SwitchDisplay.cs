using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchDisplay : MonoBehaviour
{
    public GameObject cameraX;
    public GameObject cameraY;
    public GameObject displayButton;

    private int displayX;
    private int displayY;
    private string displayText;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
        cameraX.GetComponent<Camera>().targetDisplay = 1;
        cameraY.GetComponent<Camera>().targetDisplay = 2;
        displayX = cameraX.GetComponent<Camera>().targetDisplay;
        displayY = cameraY.GetComponent<Camera>().targetDisplay;

        text = displayButton.GetComponentInChildren<TMP_Text>();
        displayText = "m1_x : m1_y";
        text.text = displayText;
    }

    public void Switch()
    {
        if (displayX == 1) {
            cameraX.GetComponent<Camera>().targetDisplay = 2;
            displayX = 2;
            displayText = "m1_y : m1_x";
        }
        else {
            cameraX.GetComponent<Camera>().targetDisplay = 1;
            displayX = 1;
            displayText = "m1_x : m1_y";
        }

        if (displayY == 1) {
            cameraY.GetComponent<Camera>().targetDisplay = 2;
            displayY = 2;
        }
        else {
            cameraY.GetComponent<Camera>().targetDisplay = 1;
            displayY = 1;
        }
        text = displayButton.GetComponentInChildren<TMP_Text>();
        text.text = displayText;
    }
}
