using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainLoop : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject[] texts;
    public GameObject[] neutrals;
    public GameObject[] desiredGroups;
    public GameObject[] noiseSliders;

    private int stage;
    private float previousAngle;
    private bool neutral;
    private bool pass;

    void Start()
    {
        // Initialize stage count
        stage = 0;
        neutral = false;
        pass = false;

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].GetComponent<TextMeshProUGUI>().text = stage.ToString();
        }

        for (int i = 0; i < neutrals.Length; i++)
        {
            neutrals[i].SetActive(true);
        }
    }

    void Update()
    {
        if ((previousAngle-targets[0].transform.localEulerAngles.z) > 10f) {
            // Deactivate cloud
            for (int i = 0; i < desiredGroups.Length; i++)
            {
                desiredGroups[i].GetComponent<DesiredGroup>().Reset(0f,0f,0f);
            }
            for (int i = 0; i < neutrals.Length; i++)
            {
                neutrals[i].SetActive(true);
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
        else if ((previousAngle-targets[0].transform.localEulerAngles.z) < -10f) {
            // Reactivate cloud
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
            if (neutral) {
                for (int i = 0; i < neutrals.Length; i++)
                {
                    neutrals[i].SetActive(false);
                }
            }
            else {
                neutral = true;
            }
        }
        previousAngle = targets[0].transform.localEulerAngles.z;
    }
}

