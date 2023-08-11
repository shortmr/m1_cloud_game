using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    public float startRange;
    public float totalRange;
    public float frameRate;
    public float screenConversion; // mm to unity position units (0.032 = 1 mm)
    public float noiseUpdate;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = (int) frameRate;
        QualitySettings.vSyncCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
