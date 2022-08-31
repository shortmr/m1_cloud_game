using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desire : MonoBehaviour
{
    public GameObject settings;
    public string subscriber;

    private float originz; //  degrees (default: -65f)
    private float gain = 100.0f; // adjusting this value will require additional changes in display
    private Quaternion targetRotation;
    private GameObject traj;
    [SerializeField] private float rotateAngle;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
        traj = GameObject.Find(subscriber);
        originz = settings.GetComponent<DisplaySettings>().startRange + 65;
    }

    // Update is called once per frame
    void Update()
    {
        rotateAngle = traj.GetComponent<JointStateSubscriber>().pos;
        targetRotation = Quaternion.Euler(0, 0, (rotateAngle*gain) + originz) * Quaternion.identity;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100);
        if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
        {
            transform.rotation = targetRotation;
        }
    }
}


