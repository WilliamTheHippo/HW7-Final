using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the moldorm stands in place for a few seconds
public class Moldorm : Enemy
{
    float randomNumber;
    float Timer;

    bool clockwise;
    float degrees;
    
    public float Lives;
    float LivesTimer;
    public float Hit;

    private float Knockback;
    private float KnockbackTimer; 
    private float KnockbackTimerReset; 

    public Vector3 KnockbackDirection;
    public Rigidbody2D Player;

    public float KnockbackSpeed;

    //should be inherited from Enemy.cs now, no?
    // public float xSpeed;
    // public float ySpeed;

    void Start()
    {
        clockwise = false;
        degrees = 0;
        KnockbackSpeed = 0f; // why is this set to 0? 
        movesDiagonal = true;
        speed = 4.5f;
        hp = 3;

        SetupEnemy();
    }

    void FixedUpdate()
    {
        //if(room != player.room) return;

        MoveInCircle();
       
        //Moves in a curve, randomly clockwise or counterclockwise about every second
        //Generate a random number from 0.0f to 1.0f;
		    //time for 1 second
        //Determine random direction.

        //TODO REPLACE THIS WITH THE ACTUAL LOGIC
        //HE USES TO SWITCH DIRECTION (ONCE WE KNOW IT)
        if (directionTimer > 1f / Time.deltaTime) SwitchDirection(); 
        directionTimer++;

        if (!isKnockback) {
            transform.Translate(direction, Space.World);
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

    void MoveInCircle()
    {
        degrees += clockwise ? Time.deltaTime : -Time.deltaTime;
        degrees %= 360;
        Debug.Log(degrees);
        transform.position /*+*/= new Vector3(
            Mathf.Cos(degrees) * 10f,
            Mathf.Sin(degrees) * 10f,
            0f
        );

    }

    void SwitchDirection() //TODO actually call this
    {
        clockwise = !clockwise;
    }
}