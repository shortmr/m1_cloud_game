using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISliderSpeed : MonoBehaviour
{

    public ParticleSystem circle;
    public GameObject main;
    public GameObject valueText;

    private int maxAmount = 100;
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

        paramSlider.value = PlayerPrefs.GetInt("speed",30);
        valueText.GetComponent<TextMeshProUGUI>().text = paramSlider.value.ToString();
    }

    public void ChangeValue()
    {
        float range = (paramSlider.value / paramSlider.maxValue) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);
        paramSlider.value = ceil * stepAmount;
        if (!main.GetComponent<MainLoop>().randomMode) {
            var main_ = circle.main;
            main_.startSpeed = paramSlider.value;
            //mainX.startSpeed = new ParticleSystem.MinMaxCurve(paramSlider.value-10, paramSlider.value+10);
            //mainY.startSpeed = new ParticleSystem.MinMaxCurve(paramSlider.value-10, paramSlider.value+10);
        }
        displayText = paramSlider.value.ToString();
        valueText.GetComponent<TextMeshProUGUI>().text = displayText;

        PlayerPrefs.SetInt("speed",(int) paramSlider.value);
    }
}
