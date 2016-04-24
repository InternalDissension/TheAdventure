using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

    public Vector3 startingPosition;

    public float speed;                 //movement speed
    public float moveAtkSpeed;          //movement speed during attacks
    public float jumpHeight;            //height of jump
    public float jumpTime;              //length of time jump takes to complete
    public float gravity;               //gravity influence
    public float dampTime = 0.15f;

    public float speedEffect;           //power of super speed
    public float speedDuration;         //length of super speed
    private bool speedEffectActive;     //is superspeed being used?

    public float speedCoolDuration;     //speed cool down length
    public float teleportCoolDuration;  //teleport cool down length

    private float movespeed;            //current speed used
    internal static float power;        //strength of powers

    //Movement
    public bool movement;               //can the player move?
    private bool forward;               //move right key
    private bool backward;              //move left key
    private bool jump;                  //jump key
    private bool crouch;                //crouch key

    private bool grounded;              //is the player on ground?
    private bool jumping;               //is the player jumping?
    private bool attacking;             //is the player attacking?
    private bool moving;                //is the player moving?

    public float startTransition;       //curve from idle to running
    public float endTransistion;        //curve from running to idle
    private float move = 0.0f;          //where we are on the curve
    private Vector3 moveVec;            //the actual movement

    private bool superspeed;            //super speed key
    private bool teleport;              //teleport key
    private bool speedcool;             //speed cooldown
    private bool teleportcool;          //teleport cooldown

    internal static bool action;        //determines if w is an action or jump
    internal bool inscene;              //used for cinematic pauses

    Animator anim;                      //Control animator component
    public Item sword;                  //Get sword on player

    Ray rray;
    Ray lray;
    RaycastHit rayhit;

    // Use this for initialization
    void Start () {
        grounded = false;    //player starts grounded
        jumping = false;    //player isn't jumping
        anim = GetComponentInChildren<Animator>();

        if (SceneManagement.GetActiveLevel() == "Level I")
            MoveOntoScene();
        else
        {
            movement = true;
            inscene = true;
            move = 0;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // = speed * Time.deltaTime;            //Movement over time rather than frames

        Vector3 reference = Vector3.zero;

        if (movement)
        {
            jump = Input.GetKeyDown(KeyCode.W);     //Is jump key being pressed
            crouch = Input.GetKey(KeyCode.S);       //Is crouch key being pressed
            forward = Input.GetKey(KeyCode.D);      //Is forward key being pressed
            backward = Input.GetKey(KeyCode.A);     //Is back key being pressed

            superspeed = Input.GetKey(KeyCode.Q) && Input.GetMouseButton(0);    //Is super speed power button being pressed
            teleport = Input.GetKey(KeyCode.Q) && Input.GetMouseButton(1);      //Is teleport power button being pressed
            attacking = Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Q);    //Is attack key being pressed
        }

        if (forward)
        {
            move += startTransition * Time.deltaTime;
            move = Mathf.Clamp(move, 0, movespeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        }

        else if (backward)
        {
            move -= startTransition * Time.deltaTime;
            move = Mathf.Clamp(move, -movespeed * Time.deltaTime, 0);

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }

        else
        {
            if (move < 0)
                if (move > -0.001)
                    move = 0;
                else
                    move += endTransistion * Time.deltaTime;

            else if (move > 0)
                if (move < 0.001)
                    move = 0;

                else
                    move -= endTransistion * Time.deltaTime;

        }

        moveVec = (Vector3.right * move);
        Vector3 newPosition = transform.position + moveVec;

        rray.origin = lray.origin = transform.position + Vector3.up;
        lray.direction = Vector3.left;
        rray.direction = Vector3.right;

        Debug.DrawRay(lray.origin, lray.direction, Color.cyan);
        Debug.DrawRay(rray.origin, rray.direction, Color.white);

        if (Physics.Raycast(lray, out rayhit, 3.0f, 1 << 9))
        {
            Debug.Log("lray " + rayhit.collider.gameObject.name);
            if (rayhit.distance <= 0.2f)
            {
                if (moveVec.x < 0)
                    newPosition = transform.position;
            }
        }

        if (Physics.Raycast(rray, out rayhit, 1.0f, 1 << 9))
        {
            Debug.Log("rray " + rayhit.collider.gameObject.name + rayhit.distance);
            if (rayhit.distance <= 0.2f)
                if (moveVec.x > 0)
                    newPosition = transform.position;
        }

        transform.position = newPosition;

        if (attacking)
        {
            movespeed = moveAtkSpeed;
            sword.attacking = true;
        }

        else if (!speedEffectActive)
        {
            sword.attacking = false;
            movespeed = speed;
        }

        if (movement)
        {
            SetBoolAnimation("running", backward || forward);
            SetBoolAnimation("attacking", attacking);
        }

        if (jump && !jumping && !action)
        {
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

        if (superspeed && !speedcool)
        {
            StartCoroutine(Dash());
        }
    }

    void MoveOntoScene()
    {
        StartCoroutine(OpeningMove());
    }

    void SetBoolAnimation(string animation, bool state)
    {
        anim.SetBool(animation, state);
    }

    IEnumerator OpeningMove()
    {
        //yield return new WaitForFixedUpdate();
        SetBoolAnimation("running", true);
        Vector3 startPos = transform.position;
        Debug.Log(startPos);
        float distance = Mathf.Abs(startingPosition.x - transform.position.x);
        Debug.Log(distance);
        float covered = 0.0f;
        while (covered < distance)
        {
            Debug.Log("Inside while");
            moveVec = Vector3.right * movespeed * Time.deltaTime;
            transform.position += moveVec;
            covered = Mathf.Abs(startPos.x - transform.position.x);
            Debug.Log(covered);
            yield return new WaitForFixedUpdate();
        }
        move = movespeed * Time.deltaTime;
        SetBoolAnimation("running", false);   
        yield return new WaitForSeconds(1.2f);
        movement = true;
        inscene = true;
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
        speedEffectActive = true;
        speedcool = true;
        float tempspeed = movespeed;
        anim.SetBool("superspeed", true);
        sword.attacking = false;
        movespeed *= speedEffect;
        move = movespeed;
        yield return new WaitForSeconds(speedDuration);
        anim.SetBool("superspeed", false);

        movespeed = tempspeed;
        move = tempspeed;
        speedEffectActive = false;
        yield return new WaitForSeconds(speedCoolDuration);
        speedcool = false;
        
    }

    IEnumerator Teleport()
    {
        teleportcool = true;
        Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = new Vector3(pos.origin.x, pos.origin.y, 0);
        yield return new WaitForSeconds(teleportCoolDuration);
        teleportcool = false;
        

    }

}
