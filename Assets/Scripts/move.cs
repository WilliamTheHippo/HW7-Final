﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    float inputHorizontal;
    float inputVertical;
    public float MoveSpeed = 5f;
    public float attackTimer = 0.5f;
    public float chargeTimer = 0f;
    public float jumpTimer = 0.45f;
    float maxSwordRayDist = 1.8f;
    public bool canMove = true;
    public bool arrowKeyPressed = false;
    public bool jumping = false;
    public bool slashing = false;
    public bool poking = false;
    int pokeCounter = 1;

    Animator linkAnimator;

    public Gel Gel;
    public Vector3 myDirection;
    public Vector3 hitDestination;
    Collider2D m_Collider; 
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        m_Collider= GetComponent<Collider2D>();
        linkAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.up = myDirection;
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        if(inputHorizontal > 0 && arrowKeyPressed == false && slashing == false){
            transform.Translate(-Time.deltaTime,0,0);
            linkAnimator.SetInteger("walkingstate", 2);
            arrowKeyPressed = true;
        }
        if(inputHorizontal < 0 && arrowKeyPressed == false && slashing == false){
            transform.Translate(Time.deltaTime,0,0);
            linkAnimator.SetInteger("walkingstate", 4);
            arrowKeyPressed = true;
        }
        if(inputVertical >0 && arrowKeyPressed == false && slashing == false){
            transform.Translate(0,-Time.deltaTime,0);
            linkAnimator.SetInteger("walkingstate", 1);
            arrowKeyPressed = true;
        }
        if(inputVertical < 0 && arrowKeyPressed == false && slashing == false){
            transform.Translate(0,Time.deltaTime,0);
            linkAnimator.SetInteger("walkingstate", 3);
            arrowKeyPressed = true;
        }
        if(inputHorizontal == 0 && inputVertical == 0){
            arrowKeyPressed = false;
            //linkAnimator.speed = 0;
        }
        if(Input.GetKey(KeyCode.Z)){
            shieldState();
        }
        if(Input.GetKeyDown(KeyCode.X)){
           slashing = true;  
        }
        if(Input.GetKey(KeyCode.X)){
            chargeTimer+= Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.X)){
            chargeTimer = 0f;
            poking = false;
            if(pokeCounter != 1){
                pokeCounter =1;
            }
           
        }
        //slashing code marked in a green debug ray
        if(slashing == true){
            Ray2D swordUpRay = new Ray2D(transform.position,transform.up);
            Ray2D swordRightRay = new Ray2D(transform.position,transform.right);
            Debug.DrawRay(swordUpRay.origin,swordUpRay.direction*maxSwordRayDist,Color.green);
            Debug.DrawRay(swordRightRay.origin,swordRightRay.direction*maxSwordRayDist,Color.green);
            RaycastHit2D SwordRayUpHit = Physics2D.Raycast(swordUpRay.origin, swordUpRay.direction,maxSwordRayDist);
            RaycastHit2D SwordRayRightHit = Physics2D.Raycast(swordRightRay.origin, swordRightRay.direction,maxSwordRayDist);
            attackTimer -= Time.deltaTime;
            MoveSpeed = 0;
            if(SwordRayRightHit.collider != null){
                //Debug.Log(SwordRayUpHit.collider.name + SwordRayRightHit.collider.name);
                if(SwordRayRightHit.collider.tag == "Enemy"){
                    //Gel.SwordHit = 1;
                    SwordRayRightHit.collider.GetComponent<Enemy>().SwordHit();
                }
            }

            if(SwordRayUpHit.collider != null){
                //Debug.Log(SwordRayUpHit.collider.name + SwordRayRightHit.collider.name);
                if(SwordRayUpHit.collider.tag == "Enemy"){
                    //Gel.SwordHit = 1;
                    SwordRayUpHit.collider.GetComponent<Enemy>().SwordHit();
                }
            }
        }
        if(attackTimer <= 0){
            slashing = false;
            MoveSpeed = 10f;
            attackTimer = 0.5f;
        }
        //poking code for when the player holds the attack button
        if(chargeTimer >= 1f && pokeCounter >= 1){
            poking = true;
            pokeCounter = 0;
        }
        if(poking == true){
            Ray2D swordUpPokeRay = new Ray2D(transform.position,transform.up);
            RaycastHit2D SwordPokeRayHit = Physics2D.Raycast(swordUpPokeRay.origin, swordUpPokeRay.direction,maxSwordRayDist);
            Debug.DrawRay(swordUpPokeRay.origin, swordUpPokeRay.direction*maxSwordRayDist ,Color.red); 
            if(SwordPokeRayHit.collider != null){
                //Debug.Log(SwordPokeRayHit.collider.name);

                if(SwordPokeRayHit.collider.tag == "Enemy"){
                     swordknockback();
                }
            }
        }

        if(Input.GetKey(KeyCode.C)){
            Debug.Log("jumpState");
            jumping = true;
        }
        if(jumping == true){
            jumpTimer -= Time.deltaTime;
            m_Collider.enabled = false;
        } else {
            m_Collider.enabled = true;
        }
        if(jumpTimer <= 0){
            Debug.Log (jumpTimer);
            jumping = false;  
            jumpTimer = 1f;
        }
    }
    void FixedUpdate(){
        myRigidBody.velocity = new Vector2 (inputHorizontal * MoveSpeed, inputVertical * MoveSpeed);
    }
    void shieldState(){
        Debug.Log("shieldState");
            Ray2D shieldRay = new Ray2D (transform.position,transform.up);
            float MaxShieldRayDist = 1.8f;
            Debug.DrawRay(shieldRay.origin, shieldRay.direction * MaxShieldRayDist, Color.blue);
            RaycastHit2D ShieldRayHit = Physics2D.Raycast(shieldRay.origin, shieldRay.direction,MaxShieldRayDist);
            if(ShieldRayHit.collider != null){
                //Debug.Log("shieldrayhit != null");
                //Debug.Log(ShieldRayHit.collider.name);
                if(ShieldRayHit.collider.tag == "Enemy"){
                    Debug.Log("shieldrayhit is hitting enemy");
                    knockback();
                }
            }  
    }
    void knockback(){
        hitDestination = -transform.up * 8;
        Vector3 hitVector = hitDestination - transform.position;
        transform.position += hitVector.normalized;
    }

    void swordknockback(){
        hitDestination = -transform.up * 8;
        Vector3 hitVector = hitDestination - transform.position;
        transform.position += hitVector.normalized;
        chargeTimer = 0f;
        poking = false;       
    }
}
