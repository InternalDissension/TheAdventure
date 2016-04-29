using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stats : MonoBehaviour {

    public Slider healthbar;
    public Text scoretext;

    public Button[] powerDisplay = new Button[] {};

    public float health;
    public int power;
    public int score;

    public float maxhealth;
    public float maxpower;

    public Color onColour;
    public Color offColour;


	// Use this for initialization
	void Start () {

        healthbar.maxValue = maxhealth;

        healthbar.minValue = 0;
        getPower = power;
	
	}
	
	// Update is called once per frame
	void Update () {

        healthbar.value = health;
        scoretext.text = score.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            power--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            power++;
        }

        getPower = power;
        power = Mathf.Clamp(power, 1, 3);

    }

    internal int getPower
    {
        get
        {
            return power;
        }

        set
        {
            power = value;

            for (int i = 0; i < power; i++)
            {
                powerDisplay[i].image.color = onColour;
            }

            for (int i = power; i < powerDisplay.Length; i++)
            {
                powerDisplay[i].image.color = offColour;
            }
        }
    }
}
