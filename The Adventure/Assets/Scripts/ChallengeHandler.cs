using UnityEngine;
using UnityEditor;
using System.Collections;

public class ChallengeHandler : MonoBehaviour {

    bool active = false;
    public int scene = 0;

    void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (scene != 0)
                {
                    SceneManagement.ActivateLevel(scene);
                }

                else
                {
                    Movement.power -= 1;
                }
            }
        }
    }

    void OnTriggerEnter()
    {
        Debug.Log("Entered");
        active = true;
        Movement.action = true;
    }

    void OnTriggerExit()
    {
        active = false;
        Movement.action = false;
    }
}
