using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{

    public Text dialogueBox;        //dialogue text box
    public Canvas dialogue_UI;      //dialogue ui canvas object
    public Transform dialogue_obj;  //object containing dialogue
    public TextMesh worldText;      //text we want displayed in the world

    public List<string> greeting;

    public string topicGreeting;
    public List<string> topics;

    public float worddelay = 0.1f;
    public bool hasTopics;          //Does the npc say anything past greeting

    private int talkCount;          //how many times dialogue has been activated
    private int greetingNum;        //which greeting will be used

    bool dialogue_possible;
    bool response;                  //has the player responded
    bool proceed;                   //Let the dialogue handler know when to proceed

    public Movement player;

    // Use this for initialization
    void Start()
    {
        dialogueBox.text = "";
        dialogue_UI.enabled = false;
        response = false;
        worldText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogue_possible)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.movement = false;
                dialogue_UI.enabled = true;
                StartCoroutine(dialogHandle());
            }
        }

    }

    void OnTriggerEnter()
    {
        dialogue_possible = true;
        worldText.text = "Press E to talk";
    }

    void OnTriggerExit()
    {
        dialogueBox.text = "";
        dialogue_UI.enabled = false;
        dialogue_possible = false;
        worldText.text = "";
    }

    IEnumerator dialogHandle()
    {
        dialogue_possible = false;

        worldText.text = "";

        if (talkCount > 0)
        {
            StartCoroutine(dialogDisplay(greeting[1]));
        }

        else
        {
            StartCoroutine(dialogDisplay(greeting[0]));
        }

        proceed = false;
        while (!Input.GetMouseButton(0) || !proceed)
        {
            yield return new WaitForFixedUpdate();
        }

        if (!hasTopics)
        {
            yield return new WaitForSeconds(0.5f);
            player.movement = true;
            dialogue_UI.enabled = false;
            dialogue_possible = true;
            StopCoroutine(dialogHandle());
        }

        else
        {

            dialogueBox.text = "";
            StartCoroutine(dialogDisplay(topicGreeting));

            proceed = false;
            while(!proceed)
            {
                yield return new WaitForFixedUpdate();
            }
            
            for (int i = 0; i < topics.Count; i++)
            {
                Debug.Log("topic");
                StartCoroutine(dialogDisplay("\n" + topics[i]));

                proceed = false;
                while (!proceed)
                {
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        talkCount++;
        worldText.text = "Press E to talk";
    }

    IEnumerator dialogDisplay(string text)
    {
        Debug.Log(text);
        for (int i = 0; i < text.Length; i++)
        {
            dialogueBox.text += text[i];

            if (i > text.Length / 4)
                if (Input.GetMouseButton(0))
                {
                    dialogueBox.text = text;
                    break;
                }
            yield return new WaitForSeconds(worddelay);
        }

        yield return new WaitForSeconds(0.5f);
        proceed = true;
    }

}
