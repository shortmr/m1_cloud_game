using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeNoise : MonoBehaviour
{
    public ParticleSystem circleX;
    public ParticleSystem circleY;
    public GameObject main;
    public GameObject noiseButton;

    private int noise;
    private string displayText;
    private TMP_Text text;
    private int[] r = {10, 30};

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
        text = noiseButton.GetComponentInChildren<TMP_Text>();
        displayText = "N" + r[0].ToString();
        text.text = displayText;
        noise = r[0];
    }

    public void Switch()
    {
        if (!main.GetComponent<MainLoop>().randomMode) {
            if (noise == r[0]) {
                noise = r[1];
                displayText = "N" + r[1].ToString();
                var shapeX = circleX.shape;
                var shapeY = circleY.shape;
                shapeX.radius = r[1];
                shapeY.radius = r[1];
            }
            else {
                noise = r[0];
                displayText = "N" + r[0].ToString();
                var shapeX = circleX.shape;
                var shapeY = circleY.shape;
                shapeX.radius = r[0];
                shapeY.radius = r[0];
            }

            text = noiseButton.GetComponentInChildren<TMP_Text>();
            text.text = displayText;
        }
    }
}