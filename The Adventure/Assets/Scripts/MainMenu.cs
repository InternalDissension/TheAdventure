using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button startbutton;
    public Button quitbutton;
    public Button progressionMode;
    public Button endlessMode;

    public Canvas TopMenu;
    public Canvas StartMenu;
    public Canvas QuitMenu;

	// Use this for initialization
	void Start () {
	
	}

    void ActivateTopMenu()
    {

    }

    void ActivateStartMenu()
    {

    }

    void ActivateQuitMenu()
    {

    }

    public void ActivateLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
