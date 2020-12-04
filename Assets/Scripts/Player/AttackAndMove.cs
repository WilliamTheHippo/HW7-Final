using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndMove : MonoBehaviour
{
    //Reef's code, implements attacks and animations
    //TODO fold get working with Alex's code
    Rigidbody2D myRigidBody;
    float inputHorizontal;
    float inputVertical;
    public float MoveSpeed = 5f;
    public float attackTimer = 0.3f;
    public float chargeTimer = 0f;
    public float spinTimer = 1f;
    public float jumpTimerUp = 0.25f;
    public float jumpTimerDown = 0.25f;
    float maxSwordRayDist = 1.8f;
    float pushRay = 1f;
    public bool canMove = true;
    public bool arrowKeyPressed = false;
    public bool jumping = false;
    public bool slashing = false;
    public bool poking = false;
    public bool shielding = false;
    public bool spinning = false;
    int pokeCounter = 1;
    public int facing;
    Animator linkAnimator;

    CameraMovement cam;
    public int room;
    //ones place holds the X coord, tens place holds the Y coord
    //THIS IS REALLY BAD PRACTICE SHIVVED IN FOR A PLAYTEST
    
    public Gel Gel;
    public Vector3 myDirection;
    public Vector3 hitDestination;
    Collider2D m_Collider; 
    void Start()
    {   
        myRigidBody = GetComponent<Rigidbody2D>();
        m_Collider= GetComponent<Collider2D>();
        linkAnimator = GetComponent<Animator>();
        cam = Camera.main.GetComponent<CameraMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        float old_x = transform.position.x;
        float old_y = transform.position.y;

        transform.up = myDirection;
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        if(inputHorizontal > 0 && arrowKeyPressed == false && slashing == false && poking == false){
            transform.Translate(-Time.deltaTime,0,0);
            linkAnimator.SetBool("walking",true);
            linkAnimator.SetFloat("AnimMoveX", inputHorizontal);
            linkAnimator.SetFloat("AnimMoveY", inputVertical);
            arrowKeyPressed = true;
            facing = 2;
        }
        if(inputHorizontal < 0 && arrowKeyPressed == false && slashing == false && poking == false){
            transform.Translate(Time.deltaTime,0,0);
            linkAnimator.SetBool("walking",true);
            linkAnimator.SetFloat("AnimMoveX", inputHorizontal);
            linkAnimator.SetFloat("AnimMoveY", inputVertical);
            arrowKeyPressed = true;
            facing = 4;
        }
        if(inputVertical > 0 && arrowKeyPressed == false && slashing == false && poking == false){
            transform.Translate(0,-Time.deltaTime,0);
            linkAnimator.SetBool("walking",true);
            linkAnimator.SetFloat("AnimMoveX", inputHorizontal);
            linkAnimator.SetFloat("AnimMoveY", inputVertical);
            arrowKeyPressed = true;
            facing = 1;
        }
        if(inputVertical < 0 && arrowKeyPressed == false && slashing == false && poking == false){
            transform.Translate(0,Time.deltaTime,0);
            linkAnimator.SetBool("walking",true);
            linkAnimator.SetFloat("AnimMoveX", inputHorizontal);
            linkAnimator.SetFloat("AnimMoveY", inputVertical);
            arrowKeyPressed = true;
            facing = 3;
        }
        if(inputHorizontal == 0 && inputVertical == 0){
            arrowKeyPressed = false;
            linkAnimator.SetBool("walking", false);
        }

        if(arrowKeyPressed){
            if(facing == 1){
                Ray2D walkUpRay = new Ray2D(transform.position,transform.up);
                Debug.DrawRay(walkUpRay.origin,walkUpRay.direction*pushRay,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(walkUpRay.origin, walkUpRay.direction,pushRay);
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Wall"){
                        linkAnimator.SetBool("pushing",true);
                    }
                } else {
                    linkAnimator.SetBool("pushing",false);
                }
            } else if (facing == 2){
                Ray2D walkRightRay = new Ray2D(transform.position,transform.right);
                Debug.DrawRay(walkRightRay.origin,walkRightRay.direction*pushRay,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(walkRightRay.origin, walkRightRay.direction,pushRay);
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Wall"){
                        linkAnimator.SetBool("pushing",true);
                    }
                } else {
                linkAnimator.SetBool("pushing",false);
                }
            } else if (facing == 3){
                Ray2D walkDownRay = new Ray2D(transform.position,-transform.up);
                Debug.DrawRay(walkDownRay.origin,walkDownRay.direction*pushRay,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(walkDownRay.origin, walkDownRay.direction,pushRay);
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Wall"){
                        linkAnimator.SetBool("pushing",true);
                    }
                } else {
                linkAnimator.SetBool("pushing",false);
                }
            } else if(facing == 4){
                Ray2D walkLeftRay = new Ray2D(transform.position,-transform.right);
                Debug.DrawRay(walkLeftRay.origin,walkLeftRay.direction*pushRay,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(walkLeftRay.origin, walkLeftRay.direction,pushRay);
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Wall"){
                        linkAnimator.SetBool("pushing",true);
                    }
                } else {
                    linkAnimator.SetBool("pushing",false);
            }
            }
        }
        if(Input.GetKeyDown(KeyCode.X)){
           slashing = true;  
        }
        if(Input.GetKey(KeyCode.X)){
            chargeTimer += Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.X)){
            if(chargeTimer >= 0.3){
                linkAnimator.SetBool("spinning", true);
                chargeTimer = 0f;
                spinning = true;
                poking = false;
            } else if (chargeTimer < 0.3){
                chargeTimer = 0f;
                poking = false;
            }
            if(pokeCounter != 1){
                pokeCounter =1;
            }
        }
        if(spinning){
            spinTimer -= Time.deltaTime;
        }
        //Debug.Log(spinTimer);
        if(spinTimer <= 0){
            linkAnimator.SetBool("spinning",false);
            linkAnimator.SetBool("poking", false);
            spinTimer = 0.5f;
            spinning = false;
        }
        if(attackTimer <= 0){
            slashing = false;
            linkAnimator.SetBool("slashing",false);
            MoveSpeed = 5f;
            attackTimer = 0.3f;
        }
        //poking code for when the player holds the attack button
        if(chargeTimer >= 0.3f && pokeCounter >= 1){
            poking = true;
            pokeCounter = 0;
        }
        //slashing code marked in a green debug ray
        if(slashing == true){
            if(facing == 1){
                Ray2D swordUpRay = new Ray2D(transform.position,transform.up);
                Ray2D swordUpRightRay = new Ray2D(transform.position,transform.up + transform.right);
                Ray2D swordRightRay = new Ray2D(transform.position,transform.right);
                Debug.DrawRay(swordUpRay.origin,swordUpRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordUpRightRay.origin, swordUpRightRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordRightRay.origin,swordRightRay.direction*maxSwordRayDist,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(swordUpRay.origin, swordUpRay.direction,maxSwordRayDist);
                RaycastHit2D SwordRayRightHit = Physics2D.Raycast(swordRightRay.origin, swordRightRay.direction,maxSwordRayDist);
                if(SwordRayRightHit.collider != null){
                    if(SwordRayRightHit.collider.tag == "Enemy"){
                        SwordRayRightHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Enemy"){
                        SwordRayUpHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
            } else if (facing == 2){
                Ray2D swordUpRay = new Ray2D(transform.position,transform.up);
                Ray2D swordUpRightRay = new Ray2D(transform.position,transform.up + transform.right);
                Ray2D swordRightRay = new Ray2D(transform.position,transform.right);
                Debug.DrawRay(swordUpRay.origin,swordUpRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordUpRightRay.origin,swordUpRightRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordRightRay.origin,swordRightRay.direction*maxSwordRayDist,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(swordUpRay.origin, swordUpRay.direction,maxSwordRayDist);
                RaycastHit2D SwordRayRightHit = Physics2D.Raycast(swordRightRay.origin, swordRightRay.direction,maxSwordRayDist);
                if(SwordRayRightHit.collider != null){
                    if(SwordRayRightHit.collider.tag == "Enemy"){
                        SwordRayRightHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Enemy"){
                        SwordRayUpHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
            } else if (facing == 3) {
                Ray2D swordDownRay = new Ray2D(transform.position,-transform.up);
                Ray2D swordDownLeftRay = new Ray2D(transform.position, -transform.up - transform.right);
                Ray2D swordLeftRay = new Ray2D(transform.position,-transform.right);
                Debug.DrawRay(swordDownRay.origin,swordDownRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordDownLeftRay.origin,swordDownLeftRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordLeftRay.origin,swordLeftRay.direction*maxSwordRayDist,Color.green);
                RaycastHit2D SwordRayDownHit = Physics2D.Raycast(swordDownRay.origin, swordDownRay.direction,maxSwordRayDist);
                RaycastHit2D SwordRayLeftHit = Physics2D.Raycast(swordLeftRay.origin, swordLeftRay.direction,maxSwordRayDist);
                if(SwordRayLeftHit.collider != null){
                    if(SwordRayLeftHit.collider.tag == "Enemy"){
                        SwordRayLeftHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
                if(SwordRayDownHit.collider != null){
                    if(SwordRayDownHit.collider.tag == "Enemy"){
                        SwordRayDownHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
            } else if (facing == 4){
                Ray2D swordUpRay = new Ray2D(transform.position,transform.up);
                Ray2D swordUpLeftRay = new Ray2D(transform.position,transform.up - transform.right);
                Ray2D swordLeftRay = new Ray2D(transform.position,-transform.right);
                Debug.DrawRay(swordUpRay.origin,swordUpRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordLeftRay.origin,swordLeftRay.direction*maxSwordRayDist,Color.green);
                Debug.DrawRay(swordUpLeftRay.origin,swordUpLeftRay.direction*maxSwordRayDist,Color.green);
                RaycastHit2D SwordRayUpHit = Physics2D.Raycast(swordUpRay.origin, swordUpRay.direction,maxSwordRayDist);
                RaycastHit2D SwordRayRightHit = Physics2D.Raycast(swordLeftRay.origin, swordLeftRay.direction,maxSwordRayDist);
                if(SwordRayRightHit.collider != null){
                    if(SwordRayRightHit.collider.tag == "Enemy"){
                        SwordRayRightHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
                if(SwordRayUpHit.collider != null){
                    if(SwordRayUpHit.collider.tag == "Enemy"){
                        SwordRayUpHit.collider.GetComponent<Enemy>().SwordHit();
                    }
                }
            }
            MoveSpeed = 0f;
            attackTimer -= Time.deltaTime;
            linkAnimator.SetBool("slashing",true); 
        }

        if(poking == true){
            linkAnimator.SetBool("poking", true);
            if(facing == 1){
                Ray2D swordUpPokeRay = new Ray2D(transform.position + new Vector3(-0.4f,0f,0f),transform.up);
                RaycastHit2D SwordPokeRayHit = Physics2D.Raycast(swordUpPokeRay.origin, swordUpPokeRay.direction,maxSwordRayDist);
                Debug.DrawRay(swordUpPokeRay.origin, swordUpPokeRay.direction*maxSwordRayDist ,Color.red); 
                if(SwordPokeRayHit.collider != null){
                    if(SwordPokeRayHit.collider.tag == "Enemy"){
                        swordknockback();
                    }
                }
            } else if (facing == 2){
                Ray2D swordUpPokeRay = new Ray2D(transform.position+ new Vector3(0f,-0.4f,0f),transform.right);
                RaycastHit2D SwordPokeRayHit = Physics2D.Raycast(swordUpPokeRay.origin, swordUpPokeRay.direction,maxSwordRayDist);
                Debug.DrawRay(swordUpPokeRay.origin, swordUpPokeRay.direction*maxSwordRayDist ,Color.red); 
                if(SwordPokeRayHit.collider != null){
                    if(SwordPokeRayHit.collider.tag == "Enemy"){
                        swordknockback();
                    }
                }
            } else if (facing == 3){
                Ray2D swordUpPokeRay = new Ray2D(transform.position + new Vector3(0.4f,0f,0f),-transform.up);
                RaycastHit2D SwordPokeRayHit = Physics2D.Raycast(swordUpPokeRay.origin, swordUpPokeRay.direction,maxSwordRayDist);
                Debug.DrawRay(swordUpPokeRay.origin, swordUpPokeRay.direction*maxSwordRayDist ,Color.red); 
                if(SwordPokeRayHit.collider != null){
                    if(SwordPokeRayHit.collider.tag == "Enemy"){
                        swordknockback();
                    }
                }
            } else if (facing == 4){
                Ray2D swordUpPokeRay = new Ray2D(transform.position + new Vector3(0f,-0.4f,0f),-transform.right);
                RaycastHit2D SwordPokeRayHit = Physics2D.Raycast(swordUpPokeRay.origin, swordUpPokeRay.direction,maxSwordRayDist);
                Debug.DrawRay(swordUpPokeRay.origin, swordUpPokeRay.direction*maxSwordRayDist ,Color.red); 
                if(SwordPokeRayHit.collider != null){
                    if(SwordPokeRayHit.collider.tag == "Enemy"){
                        swordknockback();
                    }
                }
            }
        }


        if(Input.GetKey(KeyCode.C)){
            jumping = true;
        }
        if(jumping == true){
            jumpTimerUp -= Time.deltaTime;
            transform.position += new Vector3 (0f,0.1f,0f);
            linkAnimator.SetBool("jumping",true);
            m_Collider.enabled = false;
            if(jumpTimerUp <= 0){
                transform.position -= new Vector3 (0f,0.2f,0f);
                jumpTimerDown -= Time.deltaTime;
            }
        } else {
            m_Collider.enabled = true;
        }
        if(jumpTimerDown <= 0){
            jumping = false;  
            linkAnimator.SetBool("jumping",false);
            jumpTimerUp = 0.25f;
            jumpTimerDown = 0.25f;
        }
        QuantizePosition();
        SwitchRoom(old_x, old_y);
    }
    void FixedUpdate(){
        myRigidBody.velocity = new Vector2 (inputHorizontal * MoveSpeed, inputVertical * MoveSpeed);
    }

    void QuantizePosition()
    {
        float x = Mathf.Round(transform.position.x * 8) / 8;
        float y = Mathf.Round(transform.position.y * 8) / 8;
        transform.position = new Vector3(x,y,0f);
    }

    void SwitchRoom(float old_x, float old_y)
    {
        if(Mathf.Abs(transform.position.x % 20) == 10f)
        {
            if(old_x < transform.position.x) StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Right));
            else StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Left));
            if(old_x < transform.position.x) room++;
            else room--;
        }
        if(Mathf.Abs(transform.position.y % 16) == 9f)
        {
            if(old_y < transform.position.y) StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Up));
            else StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Down));
            if(old_y < transform.position.y) room += 10;
            else room -= 10;
        }
    }

    void swordknockback(){
        hitDestination = -transform.up * 10;
        Vector3 hitVector = hitDestination - transform.position;
        transform.position += hitVector.normalized;
        chargeTimer = 0f;
        poking = false;       
        linkAnimator.SetBool("poking", false);
    }
}