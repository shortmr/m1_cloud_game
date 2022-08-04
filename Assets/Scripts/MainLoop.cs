using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainLoop : MonoBehaviour
{
    public GameObject cloudX;
    public GameObject cloudY;
    public GameObject ballX;
    public GameObject ballY;
    public GameObject textX;
    public GameObject textY;
    public int stage;

    private float previousAngle;
    private int max_stage;

    void Start()
    {
        // Initialize stage count
        stage = 0;
        max_stage = 15;
        textX.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        textY.GetComponent<TextMeshProUGUI>().text = stage.ToString();

        // Initialize pre-trial cloud
        ballX.SetActive(true);
        ballY.SetActive(true);
    }

    void Update()
    {
        if ((previousAngle-cloudX.transform.localEulerAngles.z) > 10f)
        {
            if (stage != 0) {
                // Deactivate cloud
                ballX.SetActive(false);
                ballY.SetActive(false);
            }
            if (stage < max_stage) {
                stage = stage + 1;
                textX.GetComponent<TextMeshProUGUI>().text = stage.ToString();
                textY.GetComponent<TextMeshProUGUI>().text = stage.ToString();
            }
        }
        else if ((previousAngle-cloudX.transform.localEulerAngles.z) < -10f)
        {
            // Activate cloud
            ballX.SetActive(true);
            ballY.SetActive(true);
        }
        previousAngle = cloudX.transform.localEulerAngles.z;
    }
}

