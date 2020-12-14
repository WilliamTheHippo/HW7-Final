using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{    
    [HideInInspector] public int hp = 1;
    protected Player player;
    protected Transform playerTransform;
    protected bool following;
    protected bool canKnockback = false;
    protected bool isKnockback = false;
    protected float knockbackDuration = 1f;
    protected bool hasInvFrames = false;
    protected bool movesDiagonal = false;
    protected float invTimer = 1f;
    protected float knockbackSpeed = 8f;
    protected Animator myAnimator; 
    protected Vector3 direction;
    protected Vector3 angle;
    //protected bool movesRight = true;
    protected float directionTimer = 0f;
    protected float random;

    protected float invFrames; //this never seems to be referenced, only assigned in CheckInvTimer()

    protected float xSpeed;
    protected float ySpeed;
    protected float speed = 1.5f;
    
    private float knockbackTimer;

    protected Vector2Int room;
    protected AudioSource sound;
    protected AudioClip hitSound, dieSound, fallSound;
    protected bool fallFlag = false;

    public void AssignRoom() {
        /*if(transform.parent.GetComponent<Room>() != null)
        {
            room.x = transform.parent.GetComponent<Room>().coords.x;
            room.y = transform.parent.GetComponent<Room>().coords.y;
        }*/
    }

    // All enemies except for MiniMoldorm need to call this in Start()
    public void SetupEnemy() {
        // not sure what this line does v
        // invTimer /= Time.deltaTime; //can't do this in a constructor because unity starts the clock after initialization

        player = GameObject.Find("Player").GetComponent<Player>();
        playerTransform = player.GetComponent<Transform>();

        sound = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();

        if (canKnockback) knockbackTimer = SetKnockbackTimer(knockbackDuration);
        if (hp > 1) hasInvFrames = true;

        AssignRoom();
        RandomizeDirection();
    }

    public virtual void SwordHit() 
    {
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
        } else {
            Die();
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

    void OnTriggerEnter2D(Collider2D c) => if(c.tag == "Fall") Fall();

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
        myAnimator.SetBool("isHit", true);
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

    // this function is hell !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public virtual void RandomizeDirection() 
    {
        Debug.Log(":(");
        directionTimer = 0f;
        random = Random.Range(0f, 1f);

        xSpeed = speed * Time.deltaTime;
        ySpeed = speed * Time.deltaTime;
        //movesRight = true;
        angle = new Vector3 (0f, 0f, 90f);

        if(random < 0.25f) {
            xSpeed *= -1;
            //movesRight = false;
            angle = new Vector3 (0f, 0f, 180f);
        }
        else if(random < 0.5f) { 
            ySpeed *= -1;
            angle = new Vector3 (0f, 0f, 0f);
        }
        else if(random < 0.75f) {
            xSpeed *= -1;
            ySpeed *= -1;
            //movesRight = false;
            angle = new Vector3 (0f, 0f, -90f);
        }

        if (!movesDiagonal) {
            direction = new Vector3 (xSpeed, ySpeed, 0f);

        } else if (random < 0.5f) {
            direction = new Vector3 (xSpeed, 0f, 0f);

        } else {
            direction = new Vector3 (0f, ySpeed, 0f);
        }
    } 

    public float SetKnockbackTimer(float time) => knockbackDuration / Time.deltaTime;
}