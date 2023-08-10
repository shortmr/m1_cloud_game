using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroupPoint : MonoBehaviour
{
    public GameObject settings;
    public GameObject jointCommand;
    public System.Random rand;
    public float timer = 0f;
    public float stdAngle = 0.08f; // rad
    public float stdPosition = 10f;  // mm
    public float stdVelocity = 0.07f; // rad/s

    private float originz; //  default: -65f (deg)
    private float gain = 100.0f; // approx. radian2degree * 1.745 (adjusting gain will require additional changes)
    private Quaternion startRotation;
    private Quaternion targetRotation;
    [SerializeField] private float rotateAngle;

    private float spawnTime;
    private Vector3 startPosition;
    private float convf;

    private float randAngle;
    private float randPosition;
    private float randVelocity;



    // Start is called before the first frame update
    void Start()
    {
        originz = settings.GetComponent<DisplaySettings>().startRange + 65;
        convf = settings.GetComponent<DisplaySettings>().screenConversion;
        spawnTime = 100f/1000f; // update points every 100 ms

        randAngle = 0f;
        randPosition = 0f;
        randVelocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime) {
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

            float currentAngle = jointCommand.GetComponent<JointStateSubscriber>().q;
            transform.parent.rotation = Quaternion.Euler(0, 0, ((currentAngle+randAngle)*gain) + originz) * Quaternion.identity;
            transform.localPosition = new Vector3(0f,4f+randPosition*convf,0.05f);
            timer = 0f;
        }
        else {
            transform.parent.Rotate(0,0,gain*randVelocity*Time.deltaTime);
        }
    }
}