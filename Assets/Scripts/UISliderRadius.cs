using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISliderRadius : MonoBehaviour
{

    public ParticleSystem circle;
    public GameObject main;
    public GameObject valueText;

    private int maxAmount = 60;
    private Slider paramSlider = null;
    private int numberOfSteps = 0;
    private int stepAmount = 1;
    private string displayText;

    // Start is called before the first frame update
    void Start()
    {
        paramSlider = GetComponent<Slider>();
        paramSlider.maxValue = maxAmount;
        numberOfSteps = (int) paramSlider.maxValue / stepAmount;

        paramSlider.value = PlayerPrefs.GetInt("radius",15);
        valueText.GetComponent<TextMeshProUGUI>().text = paramSlider.value.ToString();
    }

    public void ChangeValue()
    {
        float range = (paramSlider.value / paramSlider.maxValue) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);
        paramSlider.value = ceil * stepAmount;
        if (!main.GetComponent<MainLoop>().randomMode) {
            var shape_ = circle.shape;
            shape_.radius = paramSlider.value;
        }
        displayText = paramSlider.value.ToString();
        valueText.GetComponent<TextMeshProUGUI>().text = displayText;

        PlayerPrefs.SetInt("radius",(int) paramSlider.value);
    }
}
