using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardhatBeetle : Enemy
{
    public Transform playerTransform; // "public" = it will be exposed in the Unity editor inspector
    // Update is called once per frame
    private float Knockback;
    private float KnockbackTimer; //Knockback Duration
    private float KnockbackTimerReset; //Original Duration of the KnockbackTimer
    //private float KnockbackReady; //Ensures Knockback is ready

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    public float Hit;
    
    
    void Start()
    {

        Knockback = 0f;
        KnockbackTimerReset = 0.5f / Time.deltaTime;
        KnockbackTimer = KnockbackTimerReset;
        //KnockbackReady = 1;

        Hit = 0;
    }

    void Update()
    {
        var playerVector = playerTransform.position; //find player's transform position
        
        if(Knockback == 0){
            // move toward the player
            Vector3 followVector = playerVector - transform.position;
            transform.position += followVector.normalized * Time.deltaTime * 2;
        }

        //debug what the NPC is thinking
        Debug.DrawLine( transform.position, playerVector, Color.yellow );


        //Only dies by falling into a hole

        if(Hit == 1){

            Knockback = 1;
            Hit=0;        
        }

        if(Knockback == 1){
            //if(KnockbackReady == 1){
                if(KnockbackTimer > 0f){
                //Knockback
                var speed = 6f;
                KnockbackDirection = transform.position - Player.transform.position;
                transform.Translate(KnockbackDirection.normalized * speed * Time.deltaTime);
            
                }
            //}
            
            KnockbackTimer -= 1f;
        }
        if(KnockbackTimer <= 0){
            KnockbackTimer = KnockbackTimerReset;
            Knockback = 0;
        }



    }

    void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Fall"){
            Destroy(this.gameObject);
        }
	}

}
