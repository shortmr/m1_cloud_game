using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISliderRadius : MonoBehaviour
{

    public ParticleSystem circleX;
    public ParticleSystem circleY;
    public GameObject main;
    public GameObject valueText;

    private int maxAmount = 100;
    private Slider paramSlider = null;
    private int numberOfSteps = 0;
    private int stepAmount = 5;
    private string displayText;

    // Start is called before the first frame update
    void Start()
    {
        paramSlider = GetComponent<Slider>();
        paramSlider.maxValue = maxAmount;
        numberOfSteps = (int) paramSlider.maxValue / stepAmount;

        paramSlider.value = PlayerPrefs.GetInt("radius",10);
        valueText.GetComponent<TextMeshProUGUI>().text = paramSlider.value.ToString();
    }

    public void ChangeValue()
    {
        float range = (paramSlider.value / paramSlider.maxValue) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);
        paramSlider.value = ceil * stepAmount;
        if (!main.GetComponent<MainLoop>().randomMode) {
            var shapeX = circleX.shape;
            var shapeY = circleY.shape;
            shapeX.radius = paramSlider.value;
            shapeY.radius = paramSlider.value;
        }
        displayText = paramSlider.value.ToString();
        valueText.GetComponent<TextMeshProUGUI>().text = displayText;

        PlayerPrefs.SetInt("radius",(int) paramSlider.value);
    }
}
