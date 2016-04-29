using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

   static bool begin = false;
    public AudioClip dreams;
    public AudioClip ForgottenTime;
    private string level = "_Menu";

    void Awake()
    {


        if (!begin)
        {
            GetComponent<AudioSource>().Play();

            DontDestroyOnLoad(this.gameObject);
            begin = true;
        }
    }

    void Update()
    {
        if (level != SceneManagement.GetActiveLevel())
        {
            if (SceneManagement.GetActiveLevel() == "Level I")
            {
                GetComponent<AudioSource>().clip = dreams;
                GetComponent<AudioSource>().Play();
            }

            else if (SceneManagement.GetActiveLevel() == "_Menu")
            {
                GetComponent<AudioSource>().clip = ForgottenTime;
                GetComponent<AudioSource>().Play();
            }
        }

        level = SceneManagement.GetActiveLevel();
    }
}
