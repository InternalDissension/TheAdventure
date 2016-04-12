using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float hitCount;      //number of hits required to destroy the entity
    public float speed;         //how fast the entity moves
    public float hitDamage;     //how many hits are taken for every successful hit
    public float jumpHeight;    //height of the jump   
    public float knockback;     //knock back on hit
    public float knockup;       //knock up on hit

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (hitCount < 1)
        {
            Destroy(gameObject);
        }
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("See a coll");
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name.Contains("Sword"))
        {
            hitCount--;
            rb.AddForce(new Vector3(knockback, 1, 0), ForceMode.Impulse);
            
        }
    }

    void OnTriggerExit()
    {
        
    }
}
