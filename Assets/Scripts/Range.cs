using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public GameObject settings;
    public GameObject neutral;

    private float startRange; // default: -130f (deg)
    private float totalRange; // default: 110f (deg)
    private Quaternion targetAngle;
    private int segments = 25;
    private float xradius = 4;
    private float yradius = 4;
    private float gain;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        gain = settings.GetComponent<DisplaySettings>().gain*Mathf.Rad2Deg;
        startRange = settings.GetComponent<DisplaySettings>().startRange;
        totalRange = settings.GetComponent<DisplaySettings>().totalRange*gain;

        // Set neutral position
        targetAngle = Quaternion.Euler(0, 0, startRange) * Quaternion.identity;
        neutral.transform.rotation = Quaternion.Slerp(neutral.transform.rotation, targetAngle, Time.deltaTime * 100);

        // Draw angle range
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        GetComponent<Renderer>().material.color = Color.white;
        line.startColor = Color.black;
        line.endColor = Color.black;

        float x = 0f;
        float y = 0f;
        float z = 0f;
        float angle = (totalRange/2f) + startRange + 90f;

        // CW
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle -= (totalRange / segments);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
