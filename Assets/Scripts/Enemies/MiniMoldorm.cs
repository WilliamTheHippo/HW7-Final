﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// mini moldorm gets knocked back when hit
public class MiniMoldorm : Enemy
{
    float randomNumber;
    float Timer;
    public bool active;
    
    public float Lives;
    float LivesTimer;
    public float Hit;

    private float Knockback;
    private float KnockbackTimer; 
    private float KnockbackTimerReset; 

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    public float KnockbackSpeed;

    //public float xSpeed;
    //public float ySpeed;

    void Start()
    {  
        player = GameObject.Find("Player").GetComponent<Player>();//minimoldorm doesn't call SetupEnemy()
        AssignRoom();
        randomNumber = Random.Range(0.0f, 1.0f);
        Timer = 0f;

        hp = 3;
        Lives = 3;
        LivesTimer = 0f;

        Knockback = 0f;
        KnockbackTimerReset = 1f / Time.deltaTime;
        KnockbackTimer = KnockbackTimerReset;

        KnockbackSpeed = 2f;

        canKnockback = true;

        xSpeed = 1.5f * Time.deltaTime * 2.5f;
        ySpeed = 1.5f * Time.deltaTime * 2.5f;

        DeathScale = new Vector3(2f,2f,2f); //scale for the explosion'
        //SetupEnemy();

        SetupEnemy();
    }

    void FixedUpdate()
    {
        active = false;
        if(room != player.room) return;
        active = true;
       
        //Moves in a curve, randomly clockwise or counterclockwise about every second
        //Generate a random number from 0.0f to 1.0f;
		    //time for 1 second
        if (Timer > 2f / Time.deltaTime){
            randomNumber = Random.Range(0.0f, 1.0f);
            Timer = 0f;
        }
        Timer++;

        if(Knockback == 0){
            if ( randomNumber <= 0.25f ) {
                if(CheckHoles(Direction.Up) || CheckHoles(Direction.Right)) randomNumber = 0.8f;
                transform.Translate(xSpeed, 0f, 0f, Space.World); // move pixels per second
                transform.Translate(0f, ySpeed, 0f, Space.World);

                transform.localEulerAngles = new Vector3(0f, 0f, 90f );

            
            } else if (0.25f < randomNumber && randomNumber <= 0.5f) {
                if(CheckHoles(Direction.Down) || CheckHoles(Direction.Right)) randomNumber = 0.6f;
                transform.Translate(xSpeed, 0f, 0f, Space.World);
                transform.Translate( 0f, -ySpeed, 0f, Space.World);

                transform.localEulerAngles = new Vector3(0f, 0f, 0f );

            } else if (0.5f < randomNumber && randomNumber <= 0.75f) {
                if(CheckHoles(Direction.Up) || CheckHoles(Direction.Left)) randomNumber = 0.4f;
                transform.Translate(-xSpeed, 0f, 0f, Space.World);
                transform.Translate( 0f, ySpeed, 0f, Space.World);

                transform.localEulerAngles = new Vector3(0f, 0f, 180f );

            } else if (0.75f < randomNumber) {
                if(CheckHoles(Direction.Down) || CheckHoles(Direction.Left)) randomNumber = 0.1f;
                transform.Translate(-xSpeed, 0f, 0f, Space.World);
                transform.Translate( 0f, -ySpeed, 0f, Space.World);

                transform.localEulerAngles = new Vector3(0f, 0f, -90f );
            }
        }
        if (LivesTimer > 0f){
            LivesTimer -= 1f;
        }
        if(Hit == 1){
            if(LivesTimer <= 0f){
                Lives -= 1f;
                
                LivesTimer = 1f / Time.deltaTime;
            }
            if(Lives <= 0){
                Destroy(this.gameObject);
            }

            Knockback = 1;
            Hit = 0;         
        }

        if(Knockback == 1){
            if(KnockbackTimer > 0f){
                KnockbackDirection = transform.position - Player.transform.position;
                transform.Translate(KnockbackDirection.normalized * KnockbackSpeed * Time.deltaTime, Space.World);
            }
            KnockbackTimer -= 1f;
        }
        if(KnockbackTimer <= 0){
            KnockbackTimer = KnockbackTimerReset;
            Knockback = 0;
        }

    }

    public override void SwordHit() {
        Hit = 1;
        base.SwordHit();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*xSpeed = -xSpeed;
        ySpeed = -ySpeed;
        transform.Rotate(0f, 0f, 180f);*/

        if ( randomNumber <= 0.25f ) { 

                randomNumber = 0.8f;
            
            } else if (0.25f < randomNumber && randomNumber <= 0.5f) {

                randomNumber = 0.6f;

            } else if (0.5f < randomNumber && randomNumber <= 0.75f) {

                randomNumber = 0.4f;

            } else if (0.75f < randomNumber) {

                randomNumber = 0.1f;
            }

    }
}
