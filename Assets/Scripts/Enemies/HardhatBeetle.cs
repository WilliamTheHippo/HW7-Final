using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardhatBeetle : Enemy
{

    void Start()
    {
        SetupEnemy();

        noSwordHit = true;
        knockbackDuration = 0.5f / Time.deltaTime;
        knockbackSpeed = 6f;
        following = true;
        canKnockback = true;

        DeathScale = new Vector3(0.5f,0.5f,0.5f); //scale for the explosion  

    }

    void FixedUpdate()
    {
        if(room != player.room) return;

        if(! following && !isKnockback) following = true;
        
        if (isKnockback) Knockback();
        else if(following) FollowPlayer();
    }

    void OnTriggerEnter2D(Collider2D c)
	{
		/*if(c.tag == "Fall"){
            if(Knockback == 1){

            Destroy(this.gameObject);
            }
        }*/
	}


    public override void SwordHit() {
        
        following = false;
        if(canKnockback && !isKnockback) Knockback();
        //base.SwordHit();
    }
}