using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{
    public bool notHit;
    public Transform playerTransform; // "public" = it will be exposed in the Unity editor inspector
    
    //var animDie : AnimationClip; // Drag your animation from the project view in here (to inspector)

    void Start(){
        notHit = true;
    }
    void Update()
    {
        var playerVector = playerTransform.position; //find player's transform position 
        //recycle: GameObject.Find("Player").transform.position
        
        // move toward the player
        Vector3 followVector = playerVector - transform.position;
        transform.position += followVector.normalized * Time.deltaTime/4;

        //debug what the NPC is thinking
        Debug.DrawLine( transform.position, playerVector, Color.yellow);

        if(notHit == false){
            Destroy(this.gameObject);
        }
        //if (SwordHit==1){
            //animation.Play(animDie.name);
            //Destroy(this.gameObject, animDie.length);
            
        //}
    }
}
