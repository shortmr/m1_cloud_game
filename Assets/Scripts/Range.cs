using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public GameObject settings;

    private float startRange; // default (130f) degrees (this is negative in game)
    private float totalRange = 110f; // default (110f) degrees
    private int segments = 25;
    private float xradius = 4;
    private float yradius = 4;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        startRange = settings.GetComponent<DisplaySettings>().startRange;

        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        GetComponent<Renderer>().material.color = Color.white;
        line.startColor = Color.black;
        line.endColor = Color.black;

        float x = 0f;
        float y = 0f;
        float z = 0f;
        float angle = -1*startRange; //60f

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
