using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckSliders : MonoBehaviour
{
    public GameObject ballX;
    public GameObject ballY;
    public GameObject targetX;
    public GameObject targetY;
    public GameObject radiusSlider;
    public GameObject numberSlider;
    public GameObject speedSlider;
    public GameObject radiusSlider_1;
    public GameObject numberSlider_1;
    public GameObject speedSlider_1;
    public GameObject main;

    private Slider r;
    private Slider n;
    private Slider s;
    private int t_r;
    private int t_n;
    private int t_s;

    private Slider r_1;
    private Slider n_1;
    private Slider s_1;
    private int t_r_1;
    private int t_n_1;
    private int t_s_1;

    private bool check;
    private bool check_1;

    void Start()
    {
        r = radiusSlider.GetComponent<Slider>();
        n = numberSlider.GetComponent<Slider>();
        s = speedSlider.GetComponent<Slider>();

        r_1 = radiusSlider_1.GetComponent<Slider>();
        n_1 = numberSlider_1.GetComponent<Slider>();
        s_1 = speedSlider_1.GetComponent<Slider>();

        t_r = PlayerPrefs.GetInt("radius",15);
        t_n = PlayerPrefs.GetInt("max_particles",10);
        t_s = PlayerPrefs.GetInt("speed",30);

        t_r_1 = PlayerPrefs.GetInt("radius",15);
        t_n_1 = PlayerPrefs.GetInt("max_particles",10);
        t_s_1 = PlayerPrefs.GetInt("speed",30);

        if (!main.GetComponent<MainLoop>().randomMode) {
            if (t_r == 0 & t_n == 0 & t_s == 0) {
                // Deactivate cloud
                ballX.SetActive(false);
                ballY.SetActive(false);

                // Activate true target
                targetX.SetActive(true);
                targetY.SetActive(true);

                check = false;
            }
            else {
                // Activate cloud
                ballX.SetActive(true);
                ballY.SetActive(true);

                // deactivate true target
                targetX.SetActive(false);
                targetY.SetActive(false);

                check = true;
            }
        }
    }

    public void SliderValues()
    {
        if (!main.GetComponent<MainLoop>().randomMode) {
            if (r.value == 0 & n.value == 0 & s.value == 0 & check) {
                // Deactivate cloud
                ballX.SetActive(false);

                // Activate true target
                targetX.SetActive(true);

                check = false;
            }
            else if ((r.value != 0 | n.value != 0 | s.value != 0) & !check) {
                // Activate cloud
                ballX.SetActive(true);

                // deactivate true target
                targetX.SetActive(false);

                check = true;
            }

            if (r_1.value == 0 & n_1.value == 0 & s_1.value == 0 & check_1) {
                // Deactivate cloud
                ballY.SetActive(false);

                // Activate true target
                targetY.SetActive(true);

                check_1 = false;
            }
            else if ((r_1.value != 0 | n_1.value != 0 | s_1.value != 0) & !check_1) {
                // Activate cloud
                ballY.SetActive(true);

                // deactivate true target
                targetY.SetActive(false);

                check_1 = true;
            }

        }
    }
}

