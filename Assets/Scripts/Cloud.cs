using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject copy;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = copy.transform.rotation;
    }
}