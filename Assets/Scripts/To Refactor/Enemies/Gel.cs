﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{  
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }
}