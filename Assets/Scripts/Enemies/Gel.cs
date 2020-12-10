using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{  
    void Start()
    {
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
