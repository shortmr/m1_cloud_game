using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desire : MonoBehaviour
{
    public string subscriber;
    public float rotateAngle;
    public float gain;

    private Quaternion targetRotation;
    private float origionz;
    private GameObject traj;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
        origionz = -90;
        gain = 100.0f;
        traj = GameObject.Find(subscriber);
    }

    // Update is called once per frame
    void Update()
    {
        rotateAngle = traj.GetComponent<JointStateSubscriber>().pos;
        targetRotation = Quaternion.Euler(0, 0, (rotateAngle*gain) + origionz) * Quaternion.identity;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100);
        if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
        {
            transform.rotation = targetRotation;
        }
        // print(Mathf.Sin(Time.time));
    }
}


