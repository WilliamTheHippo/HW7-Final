using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{  
    void Start()
    {
        sound = GetComponent<AudioSource>();
    	AssignRoom();
        player = GameObject.Find("Player").GetComponent<Player>();
        playerTransform = player.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if(room != player.room) return;
        FollowPlayer();
    }
}
