using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchDisplay : MonoBehaviour
{
    public GameObject cameraX;
    public GameObject cameraY;

    private int displayX;
    private int displayY;
    private string displayText;
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;

        displayText = PlayerPrefs.GetString("display", "m1_x : m1_y"); // get stored display
        if (displayText == "m1_x : m1_y") {
            cameraX.GetComponent<Camera>().targetDisplay = 1;
            cameraY.GetComponent<Camera>().targetDisplay = 2;
        }
        else {
            cameraX.GetComponent<Camera>().targetDisplay = 2;
            cameraY.GetComponent<Camera>().targetDisplay = 1;
        }

        displayX = cameraX.GetComponent<Camera>().targetDisplay;
        displayY = cameraY.GetComponent<Camera>().targetDisplay;

        text = GetComponentInChildren<TMP_Text>();
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
        text = GetComponentInChildren<TMP_Text>();
        text.text = displayText;

        PlayerPrefs.SetString("display",displayText);
    }
}
