using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

    public Text dialogueBox;        //dialogue text box
    public Canvas dialogue_UI;      //dialogue ui canvas object
    public Transform dialogue_obj;  //object containing dialogue
    public TextMesh worldText;      //text we want displayed in the world

    public string firstgreeting;
    public string greeting;
    public string topic1;
    public string topic2;

    private int talkCount;          //how many times dialogue has been activated
    private int greetingNum;        //which greeting will be used

    bool dialogue_possible;
    bool response;                  //has the player responded

    public Movement player;

	// Use this for initialization
	void Start () {
        dialogueBox.text = "";
        dialogue_UI.enabled = false;
        response = false;
        worldText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

        if (dialogue_possible)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.movement = false;
                dialogue_UI.enabled = true;

                StartCoroutine(dialogue_Waiter());

                if (!string.IsNullOrEmpty(topic1))
                {
                    dialogueBox.text = topic1;
                }

                if (!string.IsNullOrEmpty(topic2))
                {
                    dialogueBox.text += "\n" + topic2;
                }
            }
        }
	
	}

    void toggleWorldText()
    {
        if (worldText.text == "")
            worldText.text = "Press E to talk";

        else
            worldText.text = "";
    }

    void OnTriggerEnter()
    {
        Debug.Log("Press E to talk");
        dialogue_possible = true;
        toggleWorldText();
    }

    void OnTriggerExit()
    {
        dialogueBox.text = "";
        dialogue_UI.enabled = false;
        dialogue_possible = false;
        worldText.text = "";
    }
    
    IEnumerator dialogue_Waiter()
    {
        while (dialogue_possible == false)
        {
            yield return null;
        }
        dialogue_possible = false;

        toggleWorldText();

        if (talkCount > 0)
        {
            dialogueBox.text = greeting;
        }

        else
        {
            dialogueBox.text = firstgreeting;
        }

        talkCount++;


    }

}
