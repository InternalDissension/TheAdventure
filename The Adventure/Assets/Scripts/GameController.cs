using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject player;
    Animator anim;
	// Use this for initialization
	void Start () {

        anim = player.GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "Level I")
        {

        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void MoveOntoScene()
    {
        //Moves the player into view at the start of the game
        

    }
}
