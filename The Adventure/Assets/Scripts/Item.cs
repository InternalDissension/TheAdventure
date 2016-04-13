using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public Collider col;
    public bool attacking;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (attacking)
        {
            col.enabled = true;
        }

        else
            col.enabled = false;
	
	}
}
