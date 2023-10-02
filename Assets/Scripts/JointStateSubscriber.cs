using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using JointState = RosMessageTypes.Geometry.Point32Msg;

public class JointStateSubscriber : MonoBehaviour
{
    private bool startup = true;
    public string topic;
    public float actual;
    public float target;
    public float start;

    void Start() {
        if (startup) {
            ROSConnection.GetOrCreateInstance().Subscribe<JointState>("/" + topic + "/", StreamData);
            startup = false;
        }
    }

    void StreamData(JointState d) {
        actual = (float)d.x;
        target = (float)d.y;
        start = (float)d.z;
    }

    private void Update()
    {

    }
}
