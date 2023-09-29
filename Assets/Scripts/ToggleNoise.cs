using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleNoise : MonoBehaviour
{
    public GameObject[] groupPoints;
    public GameObject singlePoint;

    private bool noiseActive;

    // Start is called before the first frame update
    void Start()
    {
        noiseActive = true;
        for (int i = 0; i < groupPoints.Length; i++)
        {
            groupPoints[i].SetActive(noiseActive);
        }
        singlePoint.SetActive(!noiseActive);
    }

    public void OnOff()
    {
        noiseActive = !noiseActive; //change toggle for noise
        for (int i = 0; i < groupPoints.Length; i++)
        {
            groupPoints[i].SetActive(noiseActive);
        }
        singlePoint.SetActive(!noiseActive);
    }
}
