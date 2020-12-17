using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : Enemy
{
    public bool moveLeft;

    bool move;
    bool reset;
    Vector3 resetPosition;
    float c;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        
        c = moveLeft ? -1 : 1;
        move = false;
        reset = false;
        resetPosition = transform.position;

        SetupEnemy();
    }

    void FixedUpdate()
    {
        //if(room != player.room) return;

        if(!reset)
        {
            //player within 3 units
            if((player.transform.position-this.transform.position).sqrMagnitude<4*4)
                move = true;
            if (move) transform.Translate(c * 12f * Time.deltaTime, 0f, 0f);
        }
        else
        {
            if (transform.position != resetPosition) transform.Translate(-c * 3.0f * Time.deltaTime, 0f, 0f);
            if (moveLeft && transform.position.x >= resetPosition.x) reset = false;
            if (!moveLeft && transform.position.x <= resetPosition.x) reset = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        reset = true;
        move = false;
    }
}