using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISliderNumber : MonoBehaviour
{

    public ParticleSystem circleX;
    public ParticleSystem circleY;
    public GameObject main;
    public GameObject valueText;

    private int maxAmount = 20;
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

        paramSlider.value = PlayerPrefs.GetInt("max_particles",10);
        valueText.GetComponent<TextMeshProUGUI>().text = paramSlider.value.ToString();
    }

    public void ChangeValue()
    {
        float range = (paramSlider.value / paramSlider.maxValue) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);
        paramSlider.value = ceil * stepAmount;
        if (!main.GetComponent<MainLoop>().randomMode) {
            var mainX = circleX.main;
            var mainY = circleY.main;
            mainX.maxParticles = (int) paramSlider.value;
            mainY.maxParticles = (int) paramSlider.value;
        }
        displayText = paramSlider.value.ToString();
        valueText.GetComponent<TextMeshProUGUI>().text = displayText;

        PlayerPrefs.SetInt("max_particles",(int) paramSlider.value);
    }
}

