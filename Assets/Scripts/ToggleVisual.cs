using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVisual : MonoBehaviour
{
    public GameObject[] desiredTarget;
    private bool visualActive;

    // Start is called before the first frame update
    void Start()
    {
        visualActive = true;

    }

    public void OnOff()
    {
        visualActive = !visualActive; //change toggle for visual desired
        for (int i = 0; i < desiredTarget.Length; i++)
        {
            desiredTarget[i].SetActive(visualActive);
        }
    }
}

