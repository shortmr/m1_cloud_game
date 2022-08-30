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
    public GameObject nameX;
    public GameObject nameY;
    public GameObject neutralX;
    public GameObject neutralY;
    public GameObject cond;
    public bool randomMode;

    private int stage;
    private int[] r = {15, 30, 45};
    private int[] mp = {10};
    private int[] s = {30};
    private int r_r;
    private int r_mp;
    private int r_s;
    private string tempText;

    private float previousAngle;
    private int max_stage;
    private bool neutral;
    private bool pass;

    void Start()
    {
        // Initialize stage count
        stage = 0;
        max_stage = 15;
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
            shapeX.radius = PlayerPrefs.GetInt("radius",15);
            shapeY.radius = PlayerPrefs.GetInt("radius",15);

            var mainX = circleX.main;
            var mainY = circleY.main;
            mainX.maxParticles = PlayerPrefs.GetInt("max_particles",10);
            mainY.maxParticles = PlayerPrefs.GetInt("max_particles",10);
            mainX.startSpeed = PlayerPrefs.GetInt("speed",30);
            mainY.startSpeed = PlayerPrefs.GetInt("speed",30);
        }
        else {
            r_r = Random.Range(0, r.Length);
            r_mp = Random.Range(0, mp.Length);
            r_s = Random.Range(0, s.Length);

            var shapeX = circleX.shape;
            var shapeY = circleY.shape;
            shapeX.radius = r[r_r];
            shapeY.radius = r[r_r];

            var mainX = circleX.main;
            var mainY = circleY.main;
            mainX.maxParticles = mp[r_mp];
            mainY.maxParticles = mp[r_mp];
            mainX.startSpeed = s[r_s];
            mainY.startSpeed = s[r_s];

            tempText = cond.GetComponent<TextMeshProUGUI>().text + "\n" + r[r_r].ToString() + " " + mp[r_mp].ToString() + " " + s[r_s].ToString();
            cond.GetComponent<TextMeshProUGUI>().text = tempText;
        }
    }

    void Update()
    {
        if ((previousAngle-cloudX.transform.localEulerAngles.z) > 10f)
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

                    // grey out text on final stage
                    if (stage == max_stage) {
                        textX.GetComponent<TextMeshProUGUI>().color = new Color (0.67f,0.67f,0.67f, 1.0f);
                        textY.GetComponent<TextMeshProUGUI>().color = new Color (0.67f,0.67f,0.67f, 1.0f);
                        nameX.GetComponent<TextMeshProUGUI>().color = new Color (0.67f,0.67f,0.67f, 1.0f);
                        nameY.GetComponent<TextMeshProUGUI>().color = new Color (0.67f,0.67f,0.67f, 1.0f);
                    }

                    if (randomMode) {
                        r_r = Random.Range(0, r.Length);
                        r_mp = Random.Range(0, mp.Length);
                        r_s = Random.Range(0, s.Length);

                        var shapeX = circleX.shape;
                        var shapeY = circleY.shape;
                        shapeX.radius = r[r_r];
                        shapeY.radius = r[r_r];

                        var mainX = circleX.main;
                        var mainY = circleY.main;
                        mainX.maxParticles = mp[r_mp];
                        mainY.maxParticles = mp[r_mp];
                        mainX.startSpeed = s[r_s];
                        mainY.startSpeed = s[r_s];

                        tempText = cond.GetComponent<TextMeshProUGUI>().text + "\n" + r[r_r].ToString() + " " + mp[r_mp].ToString() + " " + s[r_s].ToString();
                        cond.GetComponent<TextMeshProUGUI>().text = tempText;
                    }
                }
            }
            else {
                pass = true;
            }
        }
        else if ((previousAngle-cloudX.transform.localEulerAngles.z) < -10f)
        {
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

