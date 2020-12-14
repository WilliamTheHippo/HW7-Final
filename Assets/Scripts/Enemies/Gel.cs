using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{  
    void Start()
    {
        SetupEnemy();        
    }

    void FixedUpdate()
    {
        if(room != player.room) return;
        
        FollowPlayer();
    }
}