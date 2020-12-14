﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormFollow : Enemy
{
    public Transform BallToFollow; // ASSIGN IN INSPECTOR 
    public float LerpScale = 5f;

    Moldorm moldorm;

    void Start()
    {
        //SetupEnemy();
        moldorm = GameObject.Find("Moldorm").GetComponent<Moldorm>();
    }
    
    void FixedUpdate()
    {
        //if(room != player.room) return;

        if(BallToFollow == null) Destroy(this.gameObject);
        if(moldorm.hp <= 0) Destroy(this.gameObject);
        
        transform.position = Vector3.Lerp(transform.position, BallToFollow.position, LerpScale * Time.deltaTime); 
    }

    public override void SwordHit() {
        Debug.LogError("SwordHit() not implemented for " + gameObject.name + "!");
        base.SwordHit();
    }
}
