using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moldorm : Enemy
{
    float randomNumber;
    float Timer;
    
    void Start()
    {
        randomNumber = Random.Range(0.0f, 1.0f);
        Timer = 0f;
    }
    void Update()
    {
       
    //Moves in a curve, randomly clockwise or counterclockwise about every second
       //Generate a random number from 0.0f to 1.0f;
		    //time for 1 second
        if (Timer > 1f / Time.deltaTime){
            randomNumber = Random.Range(0.0f, 1.0f);
            Timer = 0f;
        }
        Timer++;

        if ( randomNumber <= 0.25f ) { 

            transform.Translate(1.5f * Time.deltaTime, 0f, 0f); // move pixels per second
            transform.Translate(0f, 1.5f * Time.deltaTime, 0f);

        
        } else if (0.25f < randomNumber && randomNumber <= 0.5f) {

            transform.Translate(1.5f * Time.deltaTime, 0f, 0f);
            transform.Translate( 0f, -1.5f * Time.deltaTime, 0f);

        } else if (0.5f < randomNumber && randomNumber <= 0.75f) {

            transform.Translate(-1.5f * Time.deltaTime, 0f, 0f);
            transform.Translate( 0f, 1.5f * Time.deltaTime, 0f);

        } else if (0.75f < randomNumber) {

            transform.Translate(-1.5f * Time.deltaTime, 0f, 0f);
            transform.Translate( 0f, -1.5f * Time.deltaTime, 0f);
        }

    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (randomNumber <= 0.25f){
            randomNumber = 0.8f;
        }

        if (0.25f < randomNumber && randomNumber <= 0.5f){
            randomNumber = 0.6f;
        }

        if (0.5f < randomNumber && randomNumber <= 0.75f){
            randomNumber = 0.4f;
        }

        if (0.75f < randomNumber){
            randomNumber = 0.2f;
        }
        
        /*if (collision.gameObject.tag == "Boundary")
        {
            Debug.Log("Boundary Hit!");
        }
    }*/
}