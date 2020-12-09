using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{    
    public int hp = 1;
    public Player player;
    public Transform playerTransform;
    public bool following;
    public bool canKnockback = false;
    public bool isKnockback = false;
    public float knockbackDuration = 1f;
    public bool hasInvFrames = false;
    public bool movesDiagonal = false;
    public float invTimer = 1f / Time.deltaTime;
    public float knockbackSpeed = 8f;
    public Animator myAnimator; 
    public Vector3 direction;
    public Vector3 angle;
    public bool movesRight = true;
    public float directionTimer = 0f;
    public float randomNumber;

    public float xSpeed;
    public float ySpeed;
    public float speed = 1.5f;
    
    private float knockbackTimer;

    public void SetupEnemy() {
        player = GameObject.Find("Player");
        myAnimator = GetComponent<Animator>();
        if (canKnockback) knockbackDuration = SetKnockbackTimer(knockbackDuration);
        if (hp > 1) hasInvFrames = true;
        RandomizeDirection();
    }

    public void SwordHit() 
    {
        if (hasInvFrames && CheckInvTimer()) {
            hp--;
            if (hp > 0) {
                if (canKnockback && !isKnockback) Knockback();
                myAnimator.SetBool("isHit", true);
            } else {
                Die();
                
            } 
        }
    }

    public void Fall()
    {
        //Debug.LogError("Haven't implemented falling yet!");
        Die();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Fall") Fall();
    }

    void FollowPlayer() {
        Vector3 playerVector = player.transform.position;
        Vector3 followVector = playerVector - transform.position;
        transform.position += followVector.normalized * Time.deltaTime * 2;
        Debug.DrawLine (transform.position, playerVector, Color.yellow);
    }

    void Knockback() {
        isKnockback = true;
        Vector3 KnockbackDirection = transform.position - Player.transform.position;
        transform.Translate(KnockbackDirection.normalized * knockbackSpeed * Time.deltaTime);
        knockbackTimer--;

        if (knockbackTimer <= 0) {
            knockbackTimer = SetKnockbackTimer(knockbackDuration);
            myAnimator.SetBool("isHit", false);
            isKnockback = false;
        }
    }

    void Die() {
        myAnimator.SetBool("isHit", false);
        Destroy this.GameObject();
    }

    public bool CheckInvTimer() {
        if (invTimer > 0) {
            invTimer--;
            return false;
        } else {
            invFrames = 1f / Time.deltaTime;
            return true;
        }
    }

    private void RandomizeDirection() 
    {
        directionTimer = 0f;
        randomNumber = Random.Range(0f, 1f);

        xSpeed = speed * Time.deltaTime;
        ySpeed = speed * Time.deltaTime;
        movesRight = true;
        angle = new Vector3 (0f, 0f, -45f);

        switch (random) {
            case (random < 0.25f): 
                xSpeed *= -1;
                movesRight = false;
                angle = new Vector3 (0f, 0f, 45f);
            break;
            case (random < 0.5f):  
                ySpeed *= -1;
                angle = new Vector3 (0f, 0f, -135f);
            break;
            case (random < 0.75f): 
                xSpeed *= -1;
                ySpeed *= -1;
                movesRight = false;
                angle = new Vector3 (0f, 0f, 135f);
            break;                
        }

        if (movesDiagonal) {
            direction = new Vector3 (xSpeed, ySpeed, 0f);

        } else if (random < 0.5f) {
            direction = new Vector3 (xSpeed, 0f, 0f);

        } else {
            direction = new Vector3 (0f, ySpeed, 0f);
        }
    } 

    public void SetKnockbackTimer(float time) => knockbackDuration / Time.deltaTime;
}