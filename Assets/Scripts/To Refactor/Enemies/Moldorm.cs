using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the moldorm stands in place for a few seconds
public class Moldorm : Enemy
{
    float randomNumber;
    float Timer;
    

    public float Lives;
    float LivesTimer;
    public float Hit;

    private float Knockback;
    private float KnockbackTimer; 
    private float KnockbackTimerReset; 

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    public float KnockbackSpeed;

    public float xSpeed;
    public float ySpeed;

    void Start()
    {


        KnockbackSpeed = 0f; // why is this set to 0? 

        movesDiagonal = true;
        speed = 4.5f;
        hp = 3;

        SetupEnemy();
    }

    void FixedUpdate()
    {
       
    //Moves in a curve, randomly clockwise or counterclockwise about every second
       //Generate a random number from 0.0f to 1.0f;
		    //time for 1 second
        //Determine random direction.
        if (directionTimer > 1f / Time.deltaTime) RandomizeDirection(); 
        directionTimer++;

        if (!isKnockback) {
            transform.Translate(direction);
            transform.localEulerAngles = angle;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*xSpeed = -xSpeed;
        ySpeed = -ySpeed;
        transform.Rotate(0f, 0f, 180f);*/

             if (randomNumber <= 0.25f) { randomNumber = 0.8f; } 
        else if (randomNumber <= 0.5f)  { randomNumber = 0.6f; } 
        else if (randomNumber <= 0.75f) { randomNumber = 0.4f; } 
        else                            { randomNumber = 0.1f; }

    }
}