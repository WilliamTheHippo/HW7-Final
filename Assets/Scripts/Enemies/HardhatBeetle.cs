using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardhatBeetle : Enemy
{

    void Start()
    {
        sound = GetComponent<AudioSource>();
        AssignRoom();
        knockbackDuration = 0.5f;
        knockbackSpeed = 6f;
        following = true;
        canKnockback = true;

        SetupEnemy();
    }

    void FixedUpdate()
    {
        if(room != player.room) return;
        if (following) FollowPlayer();
        if (isKnockback) Knockback();
    }

    public override void SwordHit() {
        following = false;
        base.SwordHit();
    }
}