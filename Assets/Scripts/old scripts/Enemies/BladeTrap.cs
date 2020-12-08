using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : Enemy
{
    private GameObject player;

    private float Trigger;
    private float ResetTrigger;
    private Vector3 ResetPosition;
    public float moveLeft;

    new void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        Trigger = 0;
        ResetTrigger = 0;
        ResetPosition = transform.position;
    }

    public override void OnUpdate()
    {
        if(ResetTrigger == 0)
        {
            if((player.transform.position-this.transform.position).sqrMagnitude<6*6)
            {
                // the player is within a radius of 3 units to this game object
                Trigger = 1;
            }
            if (Trigger == 1)
            {
                if (moveLeft == 0)
                {
                    transform.Translate(12f * Time.deltaTime, 0f, 0f); // move pixels per second
                }
                if (moveLeft == 1)
                {
                    transform.Translate(-12f * Time.deltaTime, 0f, 0f); // move pixels per second
                }
            }
        }
            if (ResetTrigger == 1)
            {
                if (transform.position != ResetPosition)
                {
                    if (moveLeft == 0)
                    {
                        transform.Translate(-3.0f * Time.deltaTime, 0f, 0f);
                    }
                    if (moveLeft == 1)
                    {
                        transform.Translate(3.0f * Time.deltaTime, 0f, 0f);
                    }
                }

                if (moveLeft == 0)
                {
                    if (transform.position.x <= ResetPosition.x)
                    {
                        ResetTrigger=0;
                    }
                }
                if (moveLeft == 1)
                {
                    if (transform.position.x >= ResetPosition.x)
                    {
                        ResetTrigger=0;
                    }
                }

            }
        }

        public override void SwordHit() {return;}

        void OnCollisionEnter2D(Collision2D collision)
        {
            ResetTrigger = 1;
            Trigger = 0;
        }
}