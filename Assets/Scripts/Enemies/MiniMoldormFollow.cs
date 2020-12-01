﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMoldormFollow : Enemy
{
    public Transform BallToFollow;
    public float LerpScale=5f;

    new void Start()
    {
        base.Start();
        //BallToFollow = transform.parent;
    }

    public override void OnUpdate()
    {
        if(BallToFollow == null){
            Destroy(this.gameObject);
        }
        /*GameObject EnemyMoldorm = GameObject.Find("Mini-Moldorm");
        MiniMoldorm MoldormScript = EnemyMoldorm.GetComponent<MiniMoldorm>();
        if(MoldormScript.Lives <= 0){

            Destroy(this.gameObject);
        }*/
        
        transform.position = Vector3.Lerp(transform.position, BallToFollow.position, LerpScale * Time.deltaTime);

    
    }

    public override void SwordHit() {Debug.LogError("SwordHit() not implemented for " + gameObject.name + "!");}
}
