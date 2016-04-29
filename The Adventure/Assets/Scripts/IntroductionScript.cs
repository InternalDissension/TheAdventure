using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroductionScript : MonoBehaviour {

    public Text movement;
    public Text speed;
    public Text teleport;

    public float wait;

    bool proceed;
    bool move;
    bool spd;
    bool port;

	// Use this for initialization
	void Start () {

        movement.enabled = false;
        speed.enabled = false;
        teleport.enabled = false;
        proceed = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (proceed && Movement.inscene)
        {
            if (!move)
            {
                movement.enabled = move = true;
            }

            else if (!spd)
            {
                movement.enabled = false;
                speed.enabled = spd = true;

            }

            else if (!port)
            {
                speed.enabled = false;
                teleport.enabled = port = true;
            }

            else
            {
                teleport.enabled = false;
                GetComponent<IntroductionScript>().enabled = false;
            }

            proceed = false;
            StartCoroutine(WaitForBool());
        }
	
	}

    IEnumerator WaitForBool()
    {
        yield return new WaitForSeconds(wait);
        proceed = true;
    }
}
