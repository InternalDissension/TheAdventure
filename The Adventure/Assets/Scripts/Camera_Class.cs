using UnityEngine;
using System.Collections;

public class Camera_Class : MonoBehaviour {


    public Transform player;
    public float xOffset;
    public float yOffset;
    public float dampTime = 0.15f;
    public float zoom;

    private Vector3 velocity;
	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

        Vector3 target = new Vector3(player.position.x + xOffset, player.position.y + yOffset, -zoom);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, dampTime);

    }
}
