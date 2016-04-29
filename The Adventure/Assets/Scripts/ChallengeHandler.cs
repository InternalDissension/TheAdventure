using UnityEngine;
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
                if (scene != -1)
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
        active = true;
        Movement.action = true;
    }

    void OnTriggerExit()
    {
        active = false;
        Movement.action = false;
    }
}
