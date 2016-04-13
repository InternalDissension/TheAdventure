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
    public Canvas StateMenu;

	// Use this for initialization
	void Start () {
	
	}

    public void ActivateTopMenu()
    {
        TopMenu.enabled = true;
       // QuitMenu.enabled = false;
        StartMenu.enabled = false;
        StateMenu.enabled = false;
    }

    public void ActivateStartMenu()
    {
        TopMenu.enabled = false;
        //QuitMenu.enabled = false;
        StartMenu.enabled = true;
        StateMenu.enabled = false;
    }

    public void ActivateQuitMenu()
    {
        TopMenu.enabled = false;
        //QuitMenu.enabled = true;
        StartMenu.enabled = false;
        StateMenu.enabled = false;
    }

    public void ActivateStateMenu()
    {
        TopMenu.enabled = false;
       // QuitMenu.enabled = false;
        StartMenu.enabled = false;
        StateMenu.enabled = true;
    }

    public void ActivateLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
