using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Monologue : MonoBehaviour {

    public Text dialogueBox;        //dialogue text box
    public Canvas dialogue_UI;      //dialogue ui canvas object

    public string[] text;

    public float worddelay;
    public bool onStart;            //does this monologue begin automatically
    private bool proceed;

    internal bool mono;              //is this a monologue scene

    void Start () {

        if (!onStart)
            mono = false;
        else
            mono = true;
	
	}
	
	void Update () {

        if (mono)
        {
            mono = false;
            StartCoroutine(monologueHandler());
        }
	
	}

    IEnumerator monologueHandler()
    {
        dialogue_UI.enabled = true;
        for (int i = 0; i < text.Length; i++)
        {
            StartCoroutine(dialogDisplay(text[i]));
            proceed = false;
            while (!proceed || !Input.GetMouseButtonDown(0))
            {
                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.1f);
            dialogueBox.text = "";
        }

        SceneManagement.ActivateLevel(2);
    }

    IEnumerator dialogDisplay(string text)
    {
        //Displays text with a delay between the characters
        Debug.Log(text);
        for (int i = 0; i < text.Length; i++)
        {
            dialogueBox.text += text[i];

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
