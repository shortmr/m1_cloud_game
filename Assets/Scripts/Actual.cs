using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actual : MonoBehaviour
{
    public GameObject settings;
    public GameObject jointState;

    private float originz; //  default: -65f (deg)
    private float gain = 100.0f; // approx. radian2degree * 1.745 (adjusting gain will require additional changes)
    private Quaternion targetRotation;
    private GameObject traj;
    [SerializeField] private float rotateAngle;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
        traj = jointState;
        originz = settings.GetComponent<DisplaySettings>().startRange + 65;
    }

    // Update is called once per frame
    void Update()
    {
        rotateAngle = traj.GetComponent<JointStateSubscriber>().q;
        targetRotation = Quaternion.Euler(0, 0, (rotateAngle*gain) + originz) * Quaternion.identity;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100);
        if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
        {
            transform.rotation = targetRotation;
        }
    }
}
