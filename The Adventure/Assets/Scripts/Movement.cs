using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    public Vector3 startingPosition;
    public bool inscene;

    public float speed;                 //speed of movement
    public float jumpHeight;            //height of jump
    public float jumpTime;              //length of time jump takes to complete
    public float gravity;               //gravity influence
    public float dampTime = 0.15f;

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

    Animator anim;      //Control animator component
    public Item sword;  //Get sword on player

    //Movement
    bool moving;                    //is the player moving?
    public float startTransition;
    public float endTransistion;    
    Vector3 moveVec;                //Tracks amount of movement
    float move = 0.0f;

    Ray rray;
    Ray lray;
    RaycastHit rayhit;

    // Use this for initialization
    void Start () {
        grounded = false;    //player starts grounded
        jumping = false;    //player isn't jumping
        anim = GetComponentInChildren<Animator>();
        MoveOntoScene();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // = speed * Time.deltaTime;            //Movement over time rather than frames

        Vector3 reference = Vector3.zero;

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
            move += startTransition * Time.deltaTime;
            move = Mathf.Clamp(move, 0, speed * Time.deltaTime);

            moveVec = (Vector3.right * move);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
            //transform.position += moveVec;
        }

        else if (backward)
        {
            move -= startTransition * Time.deltaTime;
            move = Mathf.Clamp(move, -speed * Time.deltaTime, 0);

            //moveVec = (Vector3.left * move);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
            //transform.position -= moveVec;
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
            sword.attacking = true;
        }

        else
        {
            sword.attacking = false;
        }

        if (movement)
        {
            SetBoolAnimation("running", backward || forward);
            SetBoolAnimation("attacking", attacking);
        }

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
            moveVec = Vector3.right * speed * Time.deltaTime;
            transform.position += moveVec;
            covered = Mathf.Abs(startPos.x - transform.position.x);
            Debug.Log(covered);
            yield return new WaitForFixedUpdate();
        }
        move = speed * Time.deltaTime;
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
