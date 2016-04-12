using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stats : MonoBehaviour {

    public Slider healthbar;
    public Slider powerbar;
    public Text scoretext;

    public float health;
    public float power;
    public int score;

    public float maxhealth;
    public float maxpower;


	// Use this for initialization
	void Start () {

        healthbar.maxValue = maxhealth;
        powerbar.maxValue = maxpower;

        healthbar.minValue = 0;
        powerbar.minValue = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

        healthbar.value = health;
        powerbar.value = power;
        scoretext.text = score.ToString();
	
	}
}
