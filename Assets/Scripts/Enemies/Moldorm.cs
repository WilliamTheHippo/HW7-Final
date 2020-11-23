using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moldorm : Enemy
{
    float randomNumber;
    float Timer;
    

    public float Lives;
    float LivesTimer;
    public float Hit;

    private float Knockback;
    private float KnockbackTimer; 
    private float KnockbackTimerReset; 

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    public float KnockbackSpeed;

    public float xSpeed;
    public float ySpeed;

    void Start()
    {
        randomNumber = Random.Range(0.0f, 1.0f);
        Timer = 0f;

        Lives = 3;
        LivesTimer = 0f;

        Knockback = 0f;
        KnockbackTimerReset = 1f / Time.deltaTime;
        KnockbackTimer = KnockbackTimerReset;

        KnockbackSpeed = 10f;

        xSpeed = 1.5f * Time.deltaTime * 3f;
        ySpeed = 1.5f * Time.deltaTime * 3f;
    }
    void Update()
    {
       
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

                transform.Translate(xSpeed, 0f, 0f); // move pixels per second
                transform.Translate(0f, ySpeed, 0f);

            
            } else if (0.25f < randomNumber && randomNumber <= 0.5f) {

                transform.Translate(xSpeed, 0f, 0f);
                transform.Translate( 0f, -ySpeed, 0f);

            } else if (0.5f < randomNumber && randomNumber <= 0.75f) {

                transform.Translate(-xSpeed, 0f, 0f);
                transform.Translate( 0f, ySpeed, 0f);

            } else if (0.75f < randomNumber) {

                transform.Translate(-xSpeed, 0f, 0f);
                transform.Translate( 0f, -ySpeed, 0f);
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

            //Debug.Log(StalfosLives);
            if(Lives <= 0){
                /*GameObject EnemyFollowMoldorm = GameObject.Find("Follow Moldorm");
                MoldormFollow MoldormFollowScript = EnemyFollowMoldorm.GetComponent<MoldormFollow>();
                MoldormFollowScript.Die = 1;*/

                Destroy(this.gameObject);
            }

            Knockback = 1;

            Hit = 0;         
        }

        if(Knockback == 1){
            if(KnockbackTimer > 0f){
                //Knockback
                KnockbackDirection = transform.position - Player.transform.position;
                transform.Translate(KnockbackDirection.normalized * KnockbackSpeed * Time.deltaTime);
            }
            KnockbackTimer -= 1f;
        }
        if(KnockbackTimer <= 0){
            KnockbackTimer = KnockbackTimerReset;
            Knockback = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        xSpeed = -xSpeed;
        ySpeed = -ySpeed;

    }
}