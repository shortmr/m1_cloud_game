using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredGroup : MonoBehaviour
{
    public GameObject[] groupPoints;
    public GameObject settings;
    private float noisef;
    private float fr;
    // Start is called before the first frame update
    void Start()
    {
        noisef = settings.GetComponent<DisplaySettings>().noiseUpdate;
        fr = settings.GetComponent<DisplaySettings>().frameRate;
        for (int i = 0; i < groupPoints.Length; i++)
        {
            groupPoints[i].GetComponent<GroupPoint>().rand = new System.Random(System.Guid.NewGuid().GetHashCode());
            groupPoints[i].GetComponent<GroupPoint>().timer = noisef/(1000f)*((float) i / (float) groupPoints.Length); // add offset to each point
        }
    }

    public void Reset(float angle, float position, float velocity)
    {
        //update noise parameters
        for (int i = 0; i < groupPoints.Length; i++)
        {
            groupPoints[i].GetComponent<GroupPoint>().stdAngle = angle*(Mathf.PI / 180f);
            groupPoints[i].GetComponent<GroupPoint>().stdPosition = position;
            groupPoints[i].GetComponent<GroupPoint>().stdVelocity = velocity*(Mathf.PI / 180f);
        }
    }

    public void Refresh(float value, int type)
    {
        //update noise parameters
        for (int i = 0; i < groupPoints.Length; i++)
        {
            if (type == 1) {
                groupPoints[i].GetComponent<GroupPoint>().stdAngle = value*(Mathf.PI / 180f);
            }
            else if (type == 2) {
                groupPoints[i].GetComponent<GroupPoint>().stdPosition = value;
            }
            else if (type == 3) {
                groupPoints[i].GetComponent<GroupPoint>().stdVelocity = value*(Mathf.PI / 180f);
            }
        }
    }
}
