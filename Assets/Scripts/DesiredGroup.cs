using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredGroup : MonoBehaviour
{
    public GameObject[] groupPoints;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < groupPoints.Length; i++)
        {
            groupPoints[i].GetComponent<GroupPoint>().rand = new System.Random(System.Guid.NewGuid().GetHashCode());
            groupPoints[i].GetComponent<GroupPoint>().timer = 0.01f*(float) i; // add offset to each point
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
