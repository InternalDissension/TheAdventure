using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    public float speed;                 //speed of movement
    public float jumpHeight;            //height of jump
    public float jumpTime;              //length of time jump takes to complete
    public float gravity;               //gravity influence

    public float speedeffect;           //power of super speed
    public float sloweffect;            //power of time control

    public float speedduration;         //length of super speed
    public float slowduration;          //length of time control

    public float speedcoolduration;     //speed cool down length
    public float slowcoolduration;      //slow cool down length
    public float teleportcoolduration;  //teleport cool down length


    bool forward;       //moving right
    bool backward;      //moving left
    bool jump;          //jumping
    bool crouch;        //crouching
    bool grounded;      //on ground
    bool jumping;       //jumping
    bool attacking;     //attacking

    public bool movement;      //can the player move

    bool superspeed;    //super speed activation
    bool slow;          //time control activation
    bool teleport;      //teleport activation
    bool speedcool;     //speed cooldown
    bool slowcool;      //slow cooldown
    bool teleportcool;  //teleport cooldown

    Animator anim;

    // Use this for initialization
    void Start () {

        grounded = false;    //player starts grounded
        jumping = false;    //player isn't jumping
        anim = GetComponentInChildren<Animator>();
        transform.rotation = Quaternion.Euler(Vector3.zero);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float move = speed * Time.deltaTime;            //Movement over time rather than frames

        if (movement)
        { 
            jump = Input.GetKeyDown(KeyCode.W);             //Is jump key being pressed
            crouch = Input.GetKey(KeyCode.S);               //Is crouch key being pressed
            forward = Input.GetKey(KeyCode.D);              //Is forward key being pressed
            backward = Input.GetKey(KeyCode.A);            //Is back key being pressed
            superspeed = Input.GetKeyDown(KeyCode.Alpha1);  //Is super speed power button being pressed
            slow = Input.GetKeyDown(KeyCode.Alpha2);        //Is time control power button being pressed
            teleport = Input.GetKeyDown(KeyCode.Alpha3);    //Is teleport power button being pressed
            attacking = Input.GetMouseButton(0);
        }

		if (forward)
		{
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
            //transform.Translate(Vector3.right * move);
		}

        else if (backward)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }

        SetBoolAnimation("running", backward || forward);
        SetBoolAnimation("attacking", attacking);

        if (jump && !jumping)
        {
            Debug.Log("jumping");
            jumping = true;
            StartCoroutine(Jumping());
        }

        if (crouch)
        {

        }

       
        if (teleport && !teleportcool)
        {
            StartCoroutine(Teleport());
        }

        if (superspeed)
        {
            StartCoroutine(Dash());
        }
    }

    void SetBoolAnimation(string animation, bool state)
    {
        anim.SetBool(animation, state);
    }

    IEnumerator Jumping()
    {
        float ticks = 0;
        float increments = jumpHeight / jumpTime * Time.deltaTime;
        
        while (ticks < jumpTime)
        {
            transform.Translate(new Vector3(0, increments, 0));
            ticks++;
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < jumpTime * 2; i++)
            yield return new WaitForFixedUpdate();

        jumping = false;

    }

    IEnumerator Dash()
    {
        float tempspeed = speed;

        speed *= speedeffect;

        yield return new WaitForSeconds(speedduration);

        speed = tempspeed;
        
    }

    IEnumerator Teleport()
    {
        teleportcool = true;
        Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = new Vector3(pos.origin.x, pos.origin.y, 0);
        yield return new WaitForSeconds(teleportcoolduration);
        teleportcool = false;
        

    }

}
