using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalfos : Enemy
{
    float randomNumber;
    float Timer;

    //private float Lives;
    
    public float StalfosLives;
    float StalfosLivesTimer;
    public float StalfosHit;

    private float Knockback;
    private float KnockbackTimer;
    private float KnockbackTimerReset;

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    void Start()
    {
        randomNumber = Random.Range(0.0f, 1.0f);
        Timer = 0f;

        //Lives = 5;

        StalfosLives = 5;
        StalfosLivesTimer = 0f;

        Knockback = 0f;
        KnockbackTimerReset = 1f / Time.deltaTime;
        KnockbackTimer = KnockbackTimerReset;
    }
    void Update()
    {
       
    //Moves randomly horizontally/vertically in a different direction every 3 seconds
       //Generate a random number from 0.0f to 1.0f;
		    //time for 1 second
        if (Timer > 1f / Time.deltaTime){
            randomNumber = Random.Range(0.0f, 1.0f);
            Timer = 0f;
        }
        Timer++;

        if(Knockback == 0){
            
            if ( randomNumber < 0.25f ) { 
                
                transform.Translate(1.5f * Time.deltaTime, 0f, 0f); // move pixels per second
            
            } else if (0.25f < randomNumber && randomNumber < 0.50f) {

                transform.Translate(-1.5f * Time.deltaTime, 0f, 0f);
                
            } else if (0.50f < randomNumber && randomNumber < .75f) {

                transform.Translate( 0f, 1.5f * Time.deltaTime, 0f);

            } else if (0.75f < randomNumber && randomNumber < 1f) {

                transform.Translate( 0f, -1.5f * Time.deltaTime, 0f);
            
            }
        }

        //Debug.Log(StalfosLives);
        if (StalfosLivesTimer > 0f){
            StalfosLivesTimer -= 1f;
        }
        if(StalfosHit == 1){

            if(StalfosLivesTimer <= 0f){
                StalfosLives -= 1f;
                //Debug.Log(StalfosLives);
                StalfosLivesTimer = 1f / Time.deltaTime;
            }

            //Debug.Log(StalfosLives);
            if(StalfosLives <= 0){
                Destroy(this.gameObject);
            }

            Knockback = 1;

            StalfosHit = 0;         
        }

        if(Knockback == 1){
            if(KnockbackTimer > 0f){
                //Knockback
                var speed = 2f * 3f;
                KnockbackDirection = transform.position - Player.transform.position;
                transform.Translate(KnockbackDirection.normalized * speed * Time.deltaTime);
                
            }
            KnockbackTimer -= 1f;
        }
        if(KnockbackTimer <= 0){
            KnockbackTimer = KnockbackTimerReset;
            Knockback = 0;
        }

    }
}
