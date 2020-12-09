﻿using System.Collections;
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
        c = moveLeft ? -1 : 1;
        move = false;
        reset = false;
        resetPosition = transform.position;
    }

    void FixedUpdate()
    {
        if(!reset)
        {
            //player within 3 units
            if((player.transform.position-this.transform.position).sqrMagnitude<6*6)
                move = 1;
            if (move) transform.Translate(c * 12f * Time.deltaTime, 0f, 0f);
        }
        else
        {
            if (transform.position != resetPosition) transform.Translate(c * 3.0f * Time.deltaTime, 0f, 0f);
            if (moveLeft && transform.position.x >= resetPosition) reset = false;
            if (!moveLeft && transform.position.x <= resetPosition) reset = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        reset = true;
        move = false;
    }
}