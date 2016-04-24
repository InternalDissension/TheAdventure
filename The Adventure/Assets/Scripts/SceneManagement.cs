using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {

    public static void ActivateLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public static string GetActiveLevel()
    {
        return SceneManager.GetActiveScene().name;
    }
}
