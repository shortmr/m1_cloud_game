using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroupPoint : MonoBehaviour
{
    public GameObject settings;
    public GameObject jointTracking;
    public System.Random rand;
    public float timer = 0f;
    public float stdAngle; // rad
    public float stdPosition; // mm
    public float stdVelocity; // rad/s

    private float originz; //  default: -90f (deg)
    private float gain; // approx. 100f = radian2degree * 1.745 (adjusting gain will require additional changes)
    private Quaternion startRotation;
    private Quaternion targetRotation;
    [SerializeField] private float currentAngle;
    [SerializeField] private float previousAngle;

    private float spawnTime;
    private Vector3 startPosition;
    private float convf;

    private float randAngle;
    private float randPosition;
    private float randVelocity;
    private float noisef;

    // Start is called before the first frame update
    void Start()
    {
        originz = settings.GetComponent<DisplaySettings>().startRange;
        convf = settings.GetComponent<DisplaySettings>().screenConversion;
        gain = settings.GetComponent<DisplaySettings>().gain*Mathf.Rad2Deg;

        randAngle = 0f;
        randPosition = 0f;
        randVelocity = 0f;
    }

    public void Replace() {
        // RNG
        // rotation
        float u1 = 1.0f-(float)rand.NextDouble(); //uniform(0,1] random floats
        float u2 = 1.0f-(float)rand.NextDouble();
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        randAngle = stdAngle * randStdNormal; //random normal(0,stdDev^2)

        // position
        u1 = 1.0f-(float)rand.NextDouble(); //uniform(0,1] random floats
        u2 = 1.0f-(float)rand.NextDouble();
        randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        randPosition = stdPosition * randStdNormal; //random normal(0,stdDev^2)

        // velocity
        u1 = 1.0f-(float)rand.NextDouble(); //uniform(0,1] random floats
        u2 = 1.0f-(float)rand.NextDouble();
        randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        randVelocity = stdVelocity * randStdNormal; //random normal(0,stdDev^2)

        transform.parent.rotation = Quaternion.Euler(0, 0, ((currentAngle+randAngle)*gain) + originz) * Quaternion.identity;
        transform.localPosition = new Vector3(0f,4f+randPosition*convf,0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = jointTracking.GetComponent<JointStateSubscriber>().target;
        transform.parent.Rotate(0,0,gain*((currentAngle-previousAngle)+(randVelocity*Time.deltaTime)));
        previousAngle = currentAngle;
    }
}