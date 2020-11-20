using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : Enemy
{
    
    private GameObject player;

    private float Trigger;
    private float ResetTrigger;
    //private float CurrentPosTrigger;

    private Vector3 ResetPosition;
    //did not work: private Vector3 CurrentPos;

    public float moveLeft;
    //choose between moving left or moving right

    //recycle: public float LerpScale=1f;

    // Start is called before the first frame update
    void Start()
    {
        //retrieve the player 
        player = GameObject.Find("Player");

        //move trigger
        Trigger = 0;
        ResetTrigger = 0;
        //CurrentPosTrigger = 0;

        ResetPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
        //did not work: var playerPosition = GameObject.Find("Player").transform.position;

            
            if (ResetTrigger == 0){
                
                if ((player.transform.position-this.transform.position).sqrMagnitude<6*6) {
                // the player is within a radius of 3 units to this game object
                    Trigger = 1;
                }

                if (Trigger == 1){
                    if (moveLeft == 0){
                        transform.Translate(12f * Time.deltaTime, 0f, 0f); // move pixels per second
                    }
                    if (moveLeft == 1){
                        transform.Translate(-12f * Time.deltaTime, 0f, 0f); // move pixels per second
                    }
                }
            }

            if (ResetTrigger == 1){
                //Reset Trap back to original position
                /*did not work: if(CurrentPosTrigger == 0){
                    CurrentPos = transform.position;
                    CurrentPosTrigger = 1;
                }*/

                if (transform.position != ResetPosition){
                    if (moveLeft == 0){
                        transform.Translate(-3.0f * Time.deltaTime, 0f, 0f);
                    }
                    if (moveLeft == 1){
                        transform.Translate(3.0f * Time.deltaTime, 0f, 0f);
                    }
                }
                
                if (moveLeft == 0){
                    if (transform.position.x <= ResetPosition.x){
                        ResetTrigger=0;
                    }
                }
                if (moveLeft == 1){
                    if (transform.position.x >= ResetPosition.x){
                        ResetTrigger=0;
                    }
                }

            }
            
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ResetTrigger = 1;
        
        //Recycle: if (collision.gameObject.tag == "Boundary")
        //Debug.Log("Object Hit!");
        Trigger = 0;

    }
}