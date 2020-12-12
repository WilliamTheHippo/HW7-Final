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
    public float invTimer = 1f;
    public float knockbackSpeed = 8f;
    public Animator myAnimator; 
    public Vector3 direction;
    public Vector3 angle;
    public bool movesRight = true;
    public float directionTimer = 0f;
    public float random;

    public float invFrames; //this never seems to be referenced, only assigned in CheckInvTimer()

    public float xSpeed;
    public float ySpeed;
    public float speed = 1.5f;
    
    private float knockbackTimer;

    public Vector2Int room;
    protected AudioSource sound;
    public AudioClip hitSound, dieSound, fallSound;
    bool fallFlag = false;

    public void AssignRoom() {
        if(transform.parent.GetComponent<Room>() != null)
        {
            room.x = transform.parent.GetComponent<Room>().coords.x;
            room.y = transform.parent.GetComponent<Room>().coords.y;
        }
    }

    public void SetupEnemy() {
        invTimer /= Time.deltaTime; //can't do this in a constructor because unity starts the clock after initialization
        player = GameObject.Find("Player").GetComponent<Player>();
        myAnimator = GetComponent<Animator>();
        if (canKnockback) knockbackTimer = SetKnockbackTimer(knockbackDuration);
        if (hp > 1) hasInvFrames = true;
        RandomizeDirection();
    }

    public virtual void SwordHit() 
    {   
        Debug.Log("enemywide hit");
        sound.clip = hitSound;
        sound.Play();
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
        //TODO fall animation
        fallFlag = true;
        sound.clip = fallSound;
        Die();
        fallFlag = false;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Fall") Fall();
    }

    public void FollowPlayer() {
        Vector3 playerVector = player.transform.position;
        Vector3 followVector = playerVector - transform.position;
        transform.position += followVector.normalized * Time.deltaTime * 2;
        Debug.DrawLine (transform.position, playerVector, Color.yellow);
    }

    public void Knockback() {
        isKnockback = true;
        Vector3 KnockbackDirection = transform.position - player.transform.position;
        transform.Translate(KnockbackDirection.normalized * knockbackSpeed * Time.deltaTime);
        knockbackTimer--;

        if (knockbackTimer <= 0) {
            knockbackTimer = SetKnockbackTimer(knockbackDuration);
            myAnimator.SetBool("isHit", false);
            isKnockback = false;
        }
    }

    public void Die() {
        if(!fallFlag) sound.clip = dieSound;
        sound.Play();
        myAnimator.SetBool("isHit", false);
        Destroy(this.gameObject);
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

    public void RandomizeDirection() 
    {
        directionTimer = 0f;
        random = Random.Range(0f, 1f);

        xSpeed = speed * Time.deltaTime;
        ySpeed = speed * Time.deltaTime;
        movesRight = true;
        angle = new Vector3 (0f, 0f, -45f);

        if(random < 0.25f) {
            xSpeed *= -1;
            movesRight = false;
            angle = new Vector3 (0f, 0f, 45f);
        }
        else if(random < 0.5f) { 
            ySpeed *= -1;
            angle = new Vector3 (0f, 0f, -135f);
        }
        else if(random < 0.75f) {
            xSpeed *= -1;
            ySpeed *= -1;
            movesRight = false;
            angle = new Vector3 (0f, 0f, 135f);
        }

        if (movesDiagonal) {
            direction = new Vector3 (xSpeed, ySpeed, 0f);

        } else if (random < 0.5f) {
            direction = new Vector3 (xSpeed, 0f, 0f);

        } else {
            direction = new Vector3 (0f, ySpeed, 0f);
        }
    } 

    public float SetKnockbackTimer(float time) => knockbackDuration / Time.deltaTime;
}