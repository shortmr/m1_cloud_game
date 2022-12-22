using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using JointState = RosMessageTypes.Sensor.JointStateMsg;

public class JointStateSubscriber : MonoBehaviour
{
    private bool startup = true;
    public string topic;
    public float q;

    void Start() {
        if (startup) {
            ROSConnection.GetOrCreateInstance().Subscribe<JointState>("/" + topic + "/", StreamData);
            startup = false;
        }
    }

    void StreamData(JointState d) {
        q = (float)d.position[0];
    }

    private void Update()
    {

    }
}
