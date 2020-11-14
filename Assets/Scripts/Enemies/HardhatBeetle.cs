using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardhatBeetle : Enemy
{
    public Transform playerTransform; // "public" = it will be exposed in the Unity editor inspector
    // Update is called once per frame
    void Update()
    {
        var playerVector = playerTransform.position; //find player's transform position
        
        // move toward the player
        Vector3 followVector = playerVector - transform.position;
        transform.position += followVector.normalized * Time.deltaTime;

        //debug what the NPC is thinking
        Debug.DrawLine( transform.position, playerVector, Color.yellow );


        //Only dies by falling into a hole

    }
}
