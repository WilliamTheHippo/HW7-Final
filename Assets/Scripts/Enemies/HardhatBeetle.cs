using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardhatBeetle : Enemy
{

    void Start()
    {

        noSwordHit = true;
        knockbackDuration = 0.5f / Time.deltaTime;
        knockbackSpeed = 4f;
        following = true;
        canKnockback = true;

        DeathScale = new Vector3(0.5f,0.5f,0.5f); //scale for the explosion  

        SetupEnemy();
    }

    void FixedUpdate()
    {
        if(room != player.room) return;

        if(! following && !isKnockback) following = true;
        
        if (isKnockback) Knockback();
        else if(following) FollowPlayer();
    }


    public override void SwordHit() {
        
        following = false;
        if(canKnockback && !isKnockback) Knockback();
        //base.SwordHit();
    }
}