using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISliderNoise : MonoBehaviour
{
    public GameObject desiredGroup;
    public GameObject valueText;
    public int type;
    public float maxAmount;

    private Slider paramSlider = null;
    private float sliderInit;
    private int numberOfSteps = 0;
    private float stepAmount = 0.1f;
    private string displayText;

    // Start is called before the first frame update
    void Start()
    {
        paramSlider = GetComponent<Slider>();
        paramSlider.maxValue = maxAmount;
        numberOfSteps = (int) (paramSlider.maxValue / stepAmount);

        if (type == 1) {
            sliderInit = PlayerPrefs.GetFloat("angle",4.58f);
        }
        else if (type == 2) {
            sliderInit = PlayerPrefs.GetFloat("position",15f);
        }
        else if (type == 3) {
            sliderInit = PlayerPrefs.GetFloat("velocity",4.01f);
        }
        paramSlider.value = sliderInit;
        desiredGroup.GetComponent<DesiredGroup>().Refresh(paramSlider.value,type);
        valueText.GetComponent<TextMeshProUGUI>().text = paramSlider.value.ToString();
    }

    public void ChangeValue()
    {
        float range = (paramSlider.value / paramSlider.maxValue) * numberOfSteps;
        int ceil = Mathf.CeilToInt(range);
        paramSlider.value = (float) ceil * stepAmount;
        desiredGroup.GetComponent<DesiredGroup>().Refresh(paramSlider.value,type);

        displayText = paramSlider.value.ToString();
        valueText.GetComponent<TextMeshProUGUI>().text = displayText;
        if (type == 1) {
            PlayerPrefs.SetFloat("angle",paramSlider.value);
        }
        else if (type == 2) {
            PlayerPrefs.SetFloat("position",paramSlider.value);
        }
        else if (type == 3) {
            PlayerPrefs.SetFloat("velocity",paramSlider.value);
        }
    }
}
