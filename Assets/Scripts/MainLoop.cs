using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

public class MainLoop : MonoBehaviour
{
    public GameObject cloudX;
    public GameObject cloudY;
    public GameObject ballX;
    public GameObject ballY;
    public GameObject targetX;
    public GameObject targetY;
    public ParticleSystem circleX;
    public ParticleSystem circleY;
    public GameObject textE;
    public GameObject textX;
    public GameObject textY;
    public GameObject neutralX;
    public GameObject neutralY;
    public GameObject cond;
    public bool randomMode;

    public int[] r = {15, 30, 45};
    public int[] mp = {10};
    public int[] s = {30};

    public int[] r_arr;
    public int[] mp_arr;
    public int[] s_arr;

    private int[] r_arr_tmp;
    private int[] mp_arr_tmp;
    private int[] s_arr_tmp;

    private int stage;
    private int r_r;
    private int r_mp;
    private int r_s;
    private string tempText;
    private int max_stage = 16;
    private int multiplier;
    private System.Random ran;

    private float previousAngle;
    private bool neutral;
    private bool pass;

    void Start()
    {
        // Initialize stage count
        stage = 0;
        neutral = false;
        pass = false;
        textE.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        textX.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        textY.GetComponent<TextMeshProUGUI>().text = stage.ToString();

        // Randomize order of noise settings
        int[] r_tmp = Enumerable.Range(0, r.Length).ToArray();
        multiplier = (int) max_stage/r.Length;
        r_arr_tmp = new int[r_tmp.Length * multiplier];
        for (int i = 0; i < r_arr_tmp.Length; i++)
          r_arr_tmp[i] = r_tmp[i / multiplier];
        ran = new System.Random();
        r_arr_tmp = r_arr_tmp.OrderBy(x => ran.Next()).ToArray();
        r_arr = new int[(int) r_arr_tmp.Length*2];
        r_arr_tmp.CopyTo(r_arr, 0);
        r_arr_tmp.CopyTo(r_arr, r_arr_tmp.Length);


        int[] mp_tmp = Enumerable.Range(0, mp.Length).ToArray();
        multiplier = (int) max_stage/mp.Length;
        mp_arr_tmp = new int[mp_tmp.Length * multiplier];
        for (int i = 0; i < mp_arr_tmp.Length; i++)
          mp_arr_tmp[i] = mp_tmp[i / multiplier];
        ran = new System.Random();
        mp_arr_tmp = mp_arr_tmp.OrderBy(x => ran.Next()).ToArray();
        mp_arr = new int[(int) mp_arr_tmp.Length*2];
        mp_arr_tmp.CopyTo(mp_arr, 0);
        mp_arr_tmp.CopyTo(mp_arr, mp_arr_tmp.Length);

        int[] s_tmp = Enumerable.Range(0, s.Length).ToArray();
        multiplier = (int) max_stage/s.Length;
        s_arr_tmp = new int[s_tmp.Length * multiplier];
        for (int i = 0; i < s_arr_tmp.Length; i++)
          s_arr_tmp[i] = s_tmp[i / multiplier];
        ran = new System.Random();
        s_arr_tmp = s_arr_tmp.OrderBy(x => ran.Next()).ToArray();
        s_arr = new int[(int) s_arr_tmp.Length*2];
        s_arr_tmp.CopyTo(s_arr, 0);
        s_arr_tmp.CopyTo(s_arr, s_arr_tmp.Length);

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
            //mainX.startSpeed = new ParticleSystem.MinMaxCurve(PlayerPrefs.GetInt("speed",30)-10, PlayerPrefs.GetInt("speed",30)+10);
            //mainY.startSpeed = new ParticleSystem.MinMaxCurve(PlayerPrefs.GetInt("speed",30)-10, PlayerPrefs.GetInt("speed",30)+10);
        }
        else {
            r_r = r_arr[stage]; //Random.Range(0, r.Length);
            r_mp = mp_arr[stage]; //Random.Range(0, mp.Length);
            r_s = s_arr[stage]; //Random.Range(0, s.Length);

            if (r[r_r] == 0 | s[r_s] == 0) {
                // Deactivate cloud
                ballX.SetActive(false);
                ballY.SetActive(false);

                // Activate true target
                targetX.SetActive(true);
                targetY.SetActive(true);
            }
            else {
                // Activate cloud
                ballX.SetActive(true);
                ballY.SetActive(true);

                // deactivate true target
                targetX.SetActive(false);
                targetY.SetActive(false);
            }

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
            //mainX.startSpeed = new ParticleSystem.MinMaxCurve(s[r_s]-10, s[r_s]+10);
            //mainY.startSpeed = new ParticleSystem.MinMaxCurve(s[r_s]-10, s[r_s]+10);

            tempText = cond.GetComponent<TextMeshProUGUI>().text + "\n(" + (stage+1).ToString() + ") " + r[r_r].ToString() + " " + mp[r_mp].ToString() + " " + s[r_s].ToString();
            cond.GetComponent<TextMeshProUGUI>().text = tempText;
        }
    }

    void Update()
    {
        if ((previousAngle-cloudX.transform.localEulerAngles.z) > 10f) {
            // Deactivate cloud
            var emissionX = circleX.emission;
            var emissionY = circleY.emission;
            emissionX.enabled = false;
            emissionY.enabled = false;
            neutralX.SetActive(true);
            neutralY.SetActive(true);
            if (pass) {
                stage = stage + 1;
                textE.GetComponent<TextMeshProUGUI>().text = stage.ToString();
                textX.GetComponent<TextMeshProUGUI>().text = stage.ToString();
                textY.GetComponent<TextMeshProUGUI>().text = stage.ToString();

                if (randomMode) {
                    r_r = r_arr[stage]; //Random.Range(0, r.Length);
                    r_mp = mp_arr[stage]; //Random.Range(0, mp.Length);
                    r_s = s_arr[stage]; //Random.Range(0, s.Length);

                    if (r[r_r] == 0 | s[r_s] == 0) {
                        // Deactivate cloud
                        ballX.SetActive(false);
                        ballY.SetActive(false);

                        // Activate true target
                        targetX.SetActive(true);
                        targetY.SetActive(true);
                    }
                    else {
                        // Activate cloud
                        ballX.SetActive(true);
                        ballY.SetActive(true);

                        // deactivate true target
                        targetX.SetActive(false);
                        targetY.SetActive(false);
                    }

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
                    //mainX.startSpeed = new ParticleSystem.MinMaxCurve(s[r_s]-10, s[r_s]+10);
                    //mainY.startSpeed = new ParticleSystem.MinMaxCurve(s[r_s]-10, s[r_s]+10);

                    tempText = cond.GetComponent<TextMeshProUGUI>().text + "\n(" + (stage+1).ToString() + ") " + r[r_r].ToString() + " " + mp[r_mp].ToString() + " " + s[r_s].ToString();
                    cond.GetComponent<TextMeshProUGUI>().text = tempText;
                }
            }
            else {
                pass = true;
            }
        }
        else if ((previousAngle-cloudX.transform.localEulerAngles.z) < -10f) {
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

