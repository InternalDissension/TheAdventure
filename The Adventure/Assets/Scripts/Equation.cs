using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Equation : MonoBehaviour {

    private float max;
    private float min;

    public int level;
    public int errorlimit;

    private int oplvl;

    private float value1;        //First value in equation
    private float value2;        //Second value in equation
    private string opstring;    //char of operation
    private int operation;      //Operation in equation (add subtract mult divi, 1 2 3 4 respectively)
    private float answer;       //Answer of equation
    private float wrongAnswer;
    private float wrongAnswer2;

    public TextMesh[] displayequation = new TextMesh[6];

    string[] operations = new string[] { " + ", " - ", " * ", " / " };

	// Use this for initialization
	void Start () {

        oplvl = Mathf.Clamp(oplvl, 0, operations.Length);

        switch(level)
        {
            case 1:
                Level1();
                break;

            case 2:
                Level2();
                break;
        }

        value1 = randomGenerator(min, max);
        value2 = randomGenerator(min, max);
        operation = randomGenerator(0, oplvl);
        solver();
        wrongAnswer = randomGenerator(answer - errorlimit, answer + errorlimit);
        wrongAnswer2 = randomGenerator(answer - errorlimit, answer + errorlimit);

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

    int randomGenerator(float min, float max, float exclude = 10000)
    {
        int value = (int)exclude;

        while (value == exclude)
        {
            value = (int)UnityEngine.Random.Range(min, max);
        }

        return value;
    }

    void solver()
    {
        switch (operation)
        {
            case 0:
                answer = value1 + value2;
                opstring = "+";
                break;

            case 1:
                answer = value1 - value2;
                opstring = "-";
                break;

            case 2:
                answer = value1 * value2;
                opstring = "*";
                break;

            case 3:
                answer = value1 / value2;
                opstring = "/";
                break;
        }
    }

    void display()
    {
        displayequation[0].text = value1.ToString();
        displayequation[1].text = opstring;
        displayequation[2].text = value2.ToString();
        displayequation[3].text = answer.ToString();
        displayequation[4].text = wrongAnswer.ToString();
        displayequation[5].text = wrongAnswer2.ToString();
    }

    public float getAnswer
    {
        get { return answer; }
    }

    void OnTriggerEnter()
    {
        display();
    }
}
