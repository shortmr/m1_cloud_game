using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actual : MonoBehaviour
{
    public GameObject settings;
    public GameObject jointTracking;

    private float originz; //  default: -65f (deg)
    private float gain; // approx. 100f = radian2degree * 1.745 (adjusting gain will require additional changes)
    private Quaternion targetRotation;
    [SerializeField] private float rotateAngle;

    // Start is called before the first frame update
    void Start()
    {
        originz = settings.GetComponent<DisplaySettings>().startRange;
        gain = settings.GetComponent<DisplaySettings>().gain*Mathf.Rad2Deg;
    }

    // Update is called once per frame
    void Update()
    {
        rotateAngle = jointTracking.GetComponent<JointStateSubscriber>().actual;
        targetRotation = Quaternion.Euler(0, 0, (rotateAngle*gain) + originz) * Quaternion.identity;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100);
        if (Quaternion.Angle(targetRotation, transform.rotation) < 1)
        {
            transform.rotation = targetRotation;
        }
    }
}
