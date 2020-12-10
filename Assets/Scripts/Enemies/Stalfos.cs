using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalfos : Enemy
{
    //float directionTimer;

    void Start()
    {
        AssignRoom();
        hp = 5;
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
        if (!isKnockback) transform.Translate(direction);        
    }
}