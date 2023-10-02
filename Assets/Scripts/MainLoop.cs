using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    public GameObject settings;
    public GameObject[] texts;
    public GameObject[] neutrals;
    public GameObject[] desiredGroups;
    public GameObject[] noiseSliders;
    public GameObject jointTracking;

    private int stage;
    private float previousAngle;
    private bool pass;
    private float originz; //  default: -90f (deg)
    private float gain; // approx. 100f = radian2degree * 1.745 (adjusting gain will require additional changes)
    private Quaternion targetRotation;
    [SerializeField] private float startAngle;

    void Start()
    {
        // gain settings
        originz = settings.GetComponent<DisplaySettings>().startRange;
        gain = settings.GetComponent<DisplaySettings>().gain*Mathf.Rad2Deg;

        // Initialize stage count
        stage = 0;
        pass = false;

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].GetComponent<TextMeshProUGUI>().text = stage.ToString();
        }
    }

    void Update()
    {
        if ((previousAngle-jointTracking.GetComponent<JointStateSubscriber>().target) > 0.5f) {
            // Deactivate cloud (if change in angle greater than 0.5 radians)
            for (int i = 0; i < desiredGroups.Length; i++)
            {
                desiredGroups[i].GetComponent<DesiredGroup>().Reset(0f,0f,0f);
            }
            // Set neutral tick
            startAngle = jointTracking.GetComponent<JointStateSubscriber>().start;
            targetRotation = Quaternion.Euler(0, 0, (startAngle*gain) + originz) * Quaternion.identity;
            for (int i = 0; i < neutrals.Length; i++)
            {
                neutrals[i].transform.rotation = Quaternion.Slerp(neutrals[i].transform.rotation, targetRotation, Time.deltaTime * 100);
            }
            if (pass) {
                stage = stage + 1;
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].GetComponent<TextMeshProUGUI>().text = stage.ToString();
                }
            }
            else {
                pass = true;
            }
        }
        else if ((previousAngle-jointTracking.GetComponent<JointStateSubscriber>().target) < -0.5f) {
            // Reactivate cloud (if change in angle less than -0.5 radians)
            for (int i = 0; i < desiredGroups.Length; i++)
            {
                int sliderIndex = 0;
                if (i == 1) {
                    sliderIndex = 3;
                }
                float angle = noiseSliders[sliderIndex].GetComponent<Slider>().value;
                float position = noiseSliders[sliderIndex+1].GetComponent<Slider>().value;
                float velocity = noiseSliders[sliderIndex+2].GetComponent<Slider>().value;
                desiredGroups[i].GetComponent<DesiredGroup>().Reset(angle,position,velocity);
            }
            // Set neutral tick
            startAngle = jointTracking.GetComponent<JointStateSubscriber>().start;
            targetRotation = Quaternion.Euler(0, 0, (startAngle*gain) + originz) * Quaternion.identity;
            for (int i = 0; i < neutrals.Length; i++)
            {
                neutrals[i].transform.rotation = Quaternion.Slerp(neutrals[i].transform.rotation, targetRotation, Time.deltaTime * 100);
            }
        }
        previousAngle = jointTracking.GetComponent<JointStateSubscriber>().target;
    }
}

