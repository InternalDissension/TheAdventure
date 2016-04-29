using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    public float hitCount;      //number of hits required to destroy the entity
    public float speed;         //how fast the entity moves
    public float hitDamage;     //how many hits are taken for every successful hit
    public float jumpHeight;    //height of the jump   
    public float knockback;     //knock back on hit
    public float knockup;       //knock up on hit
    public float stunDuration;  //how long the entity is stun after it's been hit
    public float attackRecovery;//length of time between attacks


    private bool canMove;

    internal static bool hit;

    public float hitthreshold;
    public float attackDistance;

    public Movement player;

    Ray playerRay;

    Animator anim;

	// Use this for initialization
	void Start () {

        anim = GetComponentInChildren<Animator>();
        canMove = true;
        hit = false;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (hitCount < 1)
        {
            Destroy(gameObject);
        }

        RaycastHit playerHit;

        playerRay.origin = transform.position + Vector3.up;

        if (player.transform.position.x < transform.position.x)
            playerRay.direction = Vector3.left;
        else
            playerRay.direction = Vector3.right;

        if (player.damage && player.attacking)
        {
            TakeDamage();
        }


        if (canMove)
        {

            if (Physics.Raycast(playerRay, out playerHit, 50.0f, 1 << 14))
            {
                if (playerHit.distance < attackDistance)
                {
                    StartCoroutine(Attack());
                }

                else
                {
                    

                    if (player.transform.position.x < transform.position.x)
                    {
                        anim.SetBool("running", true);
                        MoveLeft();
                    }

                    else
                    {
                        anim.SetBool("running", true);
                        MoveRight();
                    }
                }
            }
        }
	
	}

    void MoveLeft()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
    }

    void MoveRight()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);
    }

    void TakeDamage()
    {
        hit = true;
        hitCount--;
        StartCoroutine(Stun());
    }

    IEnumerator Attack()
    {
        anim.SetBool("attacking", true);
        canMove = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length + attackRecovery);
        anim.SetBool("attacking", false);
        canMove = true;
    }

    IEnumerator Stun()
    {
        Debug.Log("stunned");
        anim.SetBool("stunned", true);
        canMove = false;
        yield return new WaitForSeconds(stunDuration);
        anim.SetBool("stunned", false);
        canMove = true;
        hit = false;
    }
}
