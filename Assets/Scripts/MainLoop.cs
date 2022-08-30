using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainLoop : MonoBehaviour
{
    public GameObject cloudX;
    public GameObject cloudY;
    public ParticleSystem circleX;
    public ParticleSystem circleY;
    public GameObject textE;
    public GameObject textX;
    public GameObject textY;
    public GameObject neutralX;
    public GameObject neutralY;
    public GameObject cond;
    public bool randomMode;

    private int stage;
    private int[] r = {10, 20, 40};
    private int[] mp = {10};
    private int r_r;
    private int r_mp;

    private float previousAngle;
    private int max_stage;
    private bool running;
    private bool neutral;
    private bool pass;

    void Start()
    {
        // Initialize stage count
        stage = 0;
        max_stage = 15;
        running = false;
        neutral = false;
        pass = false;
        textE.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        textX.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        textY.GetComponent<TextMeshProUGUI>().text = stage.ToString();

        // Initialize pre-trial cloud
        var emissionX = circleX.emission; // Stores the module in a local variable
        var emissionY = circleY.emission;
        emissionX.enabled = true; // Applies the new value directly to the Particle System
        emissionY.enabled = true;

        neutralX.SetActive(true);
        neutralY.SetActive(true);

        if (!randomMode) {
            var shapeX = circleX.shape;
            var shapeY = circleY.shape;
            shapeX.radius = PlayerPrefs.GetInt("radius",10);
            shapeY.radius = PlayerPrefs.GetInt("radius",10);

            var mainX = circleX.main;
            var mainY = circleY.main;
            mainX.maxParticles = PlayerPrefs.GetInt("max_particles",10);
            mainY.maxParticles = PlayerPrefs.GetInt("max_particles",10);
        }
        else {
            r_r = Random.Range(0, r.Length);
            r_mp = Random.Range(0, mp.Length);

            var shapeX = circleX.shape;
            var shapeY = circleY.shape;
            shapeX.radius = r[r_r];
            shapeY.radius = r[r_r];

            var mainX = circleX.main;
            var mainY = circleY.main;
            mainX.maxParticles = mp[r_mp];
            mainY.maxParticles = mp[r_mp];

            cond.GetComponent<TextMeshProUGUI>().text = cond.GetComponent<TextMeshProUGUI>().text + "\n" + mp[r_mp].ToString() + " " + r[r_r].ToString();
        }
    }

    void Update()
    {
        if ((previousAngle-cloudX.transform.localEulerAngles.z) > 10f & running)
        {
            // Deactivate cloud
            var emissionX = circleX.emission;
            var emissionY = circleY.emission;
            emissionX.enabled = false;
            emissionY.enabled = false;
            neutralX.SetActive(true);
            neutralY.SetActive(true);
            if (pass) {
                if (stage < max_stage) {
                    stage = stage + 1;
                    textE.GetComponent<TextMeshProUGUI>().text = stage.ToString();
                    textX.GetComponent<TextMeshProUGUI>().text = stage.ToString();
                    textY.GetComponent<TextMeshProUGUI>().text = stage.ToString();

                    if (randomMode) {
                        r_r = Random.Range(0, r.Length);
                        r_mp = Random.Range(0, mp.Length);

                        var shapeX = circleX.shape;
                        var shapeY = circleY.shape;
                        shapeX.radius = r[r_r];
                        shapeY.radius = r[r_r];

                        var mainX = circleX.main;
                        var mainY = circleY.main;
                        mainX.maxParticles = mp[r_mp];
                        mainY.maxParticles = mp[r_mp];

                        cond.GetComponent<TextMeshProUGUI>().text = cond.GetComponent<TextMeshProUGUI>().text + "\n" + mp[r_mp].ToString() + " " + r[r_r].ToString();
                    }
                }
            }
            else {
                pass = true;
            }
        }
        else if ((previousAngle-cloudX.transform.localEulerAngles.z) < -10f)
        {
            running = true;
            // Activate cloud
            var emissionX = circleX.emission;
            var emissionY = circleY.emission;
            emissionX.enabled = true;
            emissionY.enabled = true;
            if (neutral) {
                neutralX.SetActive(false);
                neutralY.SetActive(false);
            }
            else {
                neutral = true;
            }
        }
        previousAngle = cloudX.transform.localEulerAngles.z;
    }
}

