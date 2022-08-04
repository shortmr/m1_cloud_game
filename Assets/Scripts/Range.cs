using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        GetComponent<Renderer>().material.color = Color.white;
        line.startColor = Color.black;
        line.endColor = Color.black;

        float x = 0f;
        float y = 0f;
        float z = 0f;
        float angle = 35f+90f; //60f

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle -= (105f / segments); //120
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
