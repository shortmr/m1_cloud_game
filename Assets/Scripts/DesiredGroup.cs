using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesiredGroup : MonoBehaviour
{
    public GameObject[] groupPoints;
    public GameObject settings;

    private float spawnTime;
    private Vector3 startPosition;
    private float timer;
    private float[] groupSpawns;
    private bool[] groupUpdates;
    private int[] groupUpdateCounts;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = settings.GetComponent<DisplaySettings>().noiseUpdate/1000f; // replace rate in seconds
        groupSpawns = new float[groupPoints.Length];
        groupUpdates = new bool[groupPoints.Length];
        groupUpdateCounts = new int[groupPoints.Length];
        timer = 0f;
        for (int i = 0; i < groupPoints.Length; i++)
        {
            groupPoints[i].GetComponent<GroupPoint>().rand = new System.Random(System.Guid.NewGuid().GetHashCode());
            groupUpdates[i] = false;
            groupUpdateCounts[i] = 0;
            groupSpawns[i] = spawnTime - (spawnTime*((float) i / (float) groupPoints.Length));
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

    void Update() {
        timer += Time.deltaTime;
        //replace points
        for (int i = 0; i < groupPoints.Length; i++)
        {
            if (!groupUpdates[i]) {
                if (timer > groupSpawns[i]) {
                    groupPoints[i].GetComponent<GroupPoint>().Replace();
                    groupUpdates[i] = true;
                    groupUpdateCounts[i] += 1;
                    if (i == 0) {
                        timer = 0f;
                        for (int j = 0; j < groupPoints.Length; j++) {
                            groupUpdates[j] = false;
                        }
                    }
                }
            }
        }
    }
}
