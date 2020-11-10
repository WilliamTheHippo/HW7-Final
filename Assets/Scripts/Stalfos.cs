using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalfos : Enemy
{
    float randomNumber;
    float Timer;

    private float Lives;
    
    void Start()
    {
        randomNumber = Random.Range(0.0f, 1.0f);
        Timer = 0f;

        Lives = 5;

    }
    void Update()
    {
       
    //Moves randomly horizontally/vertically in a different direction every 3 seconds
       //Generate a random number from 0.0f to 1.0f;
		    //time for 1 second
        if (Timer > 1f / Time.deltaTime){
            randomNumber = Random.Range(0.0f, 1.0f);
            Timer = 0f;
        }
        Timer++;

        if ( randomNumber < 0.25f ) { 
            
            transform.Translate(0.5f * Time.deltaTime, 0f, 0f); // move pixels per second
        
        } else if (0.25f < randomNumber && randomNumber < 0.50f) {

            transform.Translate(-0.5f * Time.deltaTime, 0f, 0f);
            
		} else if (0.50f < randomNumber && randomNumber < .75f) {

            transform.Translate( 0f, 0.5f * Time.deltaTime, 0f);

        } else if (0.75f < randomNumber && randomNumber < 1f) {

            transform.Translate( 0f, -0.5f * Time.deltaTime, 0f);
		
        }

        if (SwordHit == 1){
            Lives -= 1;
            SwordHit = 0;
        }
    }
}
