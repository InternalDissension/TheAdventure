using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Equation : MonoBehaviour {

    private float max;
    private float min;

    public float value1;        //First value in equation
    public float value2;        //Second value in equation
    public string operation;    //Operation in equation
    private float answer;       //Answer of equation

    public TextMesh[] displayequation = new TextMesh[5];

	// Use this for initialization
	void Start () {

        Level1();
        randomGenerator(min, max);
	
	}
	
	// Update is called once per frame
	void Update () {

        display();
	
	}

    void EquationController()
    {

    }

    void Level1()   //Addition
    {
        min = 0;
        max = 100;
    }

    void Level2()   //Subtraction
    {
        min = 0;
        max = 100;
    }

    void Level3()   //Negatives
    {
        min = -100;
        max = 100;
    }

    void Level4()   //Multiplication
    {
        min = 0;
        max = 100;
    }

    void Level5()   //Multiplication with Negatives
    {
        min = -100;
        max = 100;
    }

    void randomGenerator(float min, float max)
    {
        value1 = (int)UnityEngine.Random.Range(min, max);
        value2 = (int)UnityEngine.Random.Range(min, max);
    }

    void display()
    {
        displayequation[0].text = value1.ToString();
        displayequation[1].text = operation;
        displayequation[2].text = value2.ToString();
        displayequation[3].text = "=";
        displayequation[4].text = answer.ToString();
    }
}
