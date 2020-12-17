using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalfos : Enemy
{
    //float directionTimer;

    void Start()
    {
        
        hp = 5;
        sound = GetComponent<AudioSource>();
        knockbackDuration = 1f / Time.deltaTime;
        knockbackSpeed = 6f;
        canKnockback = true;
        
        AssignRoom();

        DeathScale = new Vector3(1f,1f,1f); //scale for the explosion'

        xSpeed = 1.5f * Time.deltaTime * 1f;
        ySpeed = 1.5f * Time.deltaTime * 1f;

        SetupEnemy();


    }
    void Update()
    {
        if(room != player.room) return;

        //Determine random direction.
        if (directionTimer > 1f / Time.deltaTime) RandomizeDirection(); 
        directionTimer++;

        // If the hostile is not currently being knocked back, 
        // Translate in predetermined random direction. 
        //if (isKnockback) { Knockback(); } 
        //else             { transform.Translate(direction); }
        
        if (!isKnockback){
            myAnimator.SetBool("isHit", false);
            transform.Translate(direction); 
        }     

        if (isKnockback) Knockback();   
    }

    public virtual void RandomizeDirection() {
        directionTimer = 0f; //stalfos switches direction less often than moldorm does
        /*float???*/ random = Random.Range(0f,1f);
        xSpeed = speed * Time.deltaTime;
        ySpeed = speed * Time.deltaTime;
        if(random < 0.25f) direction = new Vector3(-xSpeed,0f,0f);
        else if(random < 0.5f) direction = new Vector3(xSpeed,0f,0f);
        else if(random < 0.75f) direction = new Vector3(0f,ySpeed,0f);
        else direction = new Vector3(0f,-ySpeed,0f);
    }

    /*public override void SwordHit() {
        
        //following = false;
        if(canKnockback && !isKnockback) Knockback();
        base.SwordHit();
    }*/
}