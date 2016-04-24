using UnityEngine;
using System.Collections;

public class Camera_Class : MonoBehaviour {


    public Transform player;
    public float xOffset;
    public float yOffset;
    public float dampTime = 0.15f;
    public float zoom;

    public Movement m;

    private Vector3 velocity;

	void Start () {

        Vector3 target = new Vector3(m.startingPosition.x + xOffset, m.startingPosition.y + yOffset, -zoom);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, dampTime);

    }
	
	void FixedUpdate () {

        if (m.inscene)
        {
            Vector3 target = new Vector3(player.position.x + xOffset, player.position.y + yOffset, -zoom);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, dampTime);
        }

    }
}
