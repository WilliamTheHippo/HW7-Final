using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardhatBeetle : Enemy
{
    GameObject player;
    private float Knockback;
    private float KnockbackTimer;
    private float KnockbackTimerReset;

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    public float Hit;
    
    
    void Start()
    {
        player = GameObject.Find("Player");
        Knockback = 0f;
        KnockbackTimerReset = 0.5f / Time.deltaTime;
        KnockbackTimer = KnockbackTimerReset;
        Hit = 0;
    }

    void FixedUpdate()
    {
        Vector3 playerVector = player.transform.position;
        if(Knockback == 0)
        {
            // move toward the player
            Vector3 followVector = playerVector - transform.position;
            transform.position += followVector.normalized * Time.deltaTime * 2;
        }

        Debug.DrawLine( transform.position, playerVector, Color.yellow );
        if(Hit == 1){

            Knockback = 1;
            Hit=0;        
        }

        if(Knockback == 1)
        {
            if(KnockbackTimer > 0f)
            {
                //Knockback
                float speed = 5f;
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

    public override void SwordHit() {Debug.LogError("SwordHit() not implemented for " + gameObject.name + "!");}
}
