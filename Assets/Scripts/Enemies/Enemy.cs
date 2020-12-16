using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{    
    public int hp = 1;
    protected Player player;
    protected Transform playerTransform;
    protected bool following;
    protected bool canKnockback = false;
    protected bool isKnockback = false;
    protected float knockbackDuration;
    protected bool hasInvFrames = false;
    protected bool movesDiagonal = false;
    protected float invTimer = 1f;
    protected float knockbackSpeed = 8f;
    protected bool noSwordHit = false;
    protected Animator myAnimator; 
    protected Vector3 direction;
    protected Vector3 angle;
    //protected bool movesRight = true;
    protected float directionTimer = 0f;
    protected float random;
    public GameObject deathAnimation;
    public Sprite fallSprite;

    bool alreadyHitting;

    protected float invFrames; //this never seems to be referenced, only assigned in CheckInvTimer()

    protected float xSpeed;
    protected float ySpeed;
    protected float speed = 1.5f;
    
    private float knockbackTimer;

    protected Vector2Int room;

    protected Vector3 DeathScale;

    protected AudioSource sound;
    protected AudioClip hitSound, dieSound, fallSound;
    //protected bool fallFlag = false;

    protected enum Direction {Up, Down, Left, Right}

    public void AssignRoom() {
        if(transform.parent.GetComponent<Room>() != null)
        {
            room.x = transform.parent.GetComponent<Room>().coords.x;
            room.y = transform.parent.GetComponent<Room>().coords.y;
        }
    }

    // All enemies except for MiniMoldorm need to call this in Start()
    public void SetupEnemy() {
        // not sure what this line does v
        invTimer /= Time.deltaTime; //can't do this in a constructor because unity starts the clock after initialization

        player = GameObject.Find("Player").GetComponent<Player>();
        playerTransform = player.GetComponent<Transform>();

        sound = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();

        if (canKnockback) knockbackTimer = SetKnockbackTimer(knockbackDuration);
        if (hp > 1) hasInvFrames = true;

        knockbackDuration = 1f / Time.deltaTime;
        alreadyHitting = false;

        AssignRoom();
        RandomizeDirection();
    }

    public virtual void SwordHit() 
    {
        if(alreadyHitting) return;
        if(player.currentState is Hit) alreadyHitting = true;
        sound.clip = hitSound;
        sound.Play();
        if (hasInvFrames) {
            /*if(!noSwordHit)*/ hp--;
            if (hp > 0) {
                if (canKnockback && !isKnockback) Knockback();
                myAnimator.SetBool("isHit", true);
                StartCoroutine(InvFrames());
            } else {
                StartCoroutine(Die(false));       
            } 
        } else {
            StartCoroutine(Die(false));
        }
    }

    IEnumerator InvFrames()
    {
        yield return new WaitForSeconds(0.5f);
        alreadyHitting = false;
    }

    public void Fall()
    {
        //fallFlag = true;
        //sound.clip = fallSound;
        StartCoroutine(Die(true));
        //fallFlag = false; //don't actually need this if we're just destroying the object
    }

    public void FollowPlayer() {
        Vector3 playerVector = player.transform.position;
        Vector3 followVector = playerVector - transform.position;
        float truncatedX = followVector.x;
        float truncatedY = followVector.y;
        if(CheckHoles(Direction.Up) && truncatedY > 0f) truncatedY = 0f;
        if(CheckHoles(Direction.Down) && truncatedY < 0f) truncatedY = 0f;
        if(CheckHoles(Direction.Left) && truncatedX < 0f) truncatedX = 0f;
        if(CheckHoles(Direction.Right) && truncatedX > 0f) truncatedX = 0f;
        Vector3 truncated = new Vector3(truncatedX, truncatedY, 0f);
        transform.position += truncated.normalized * Time.deltaTime * 2;
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

    public IEnumerator Die(bool falling) {
        if(!falling)
        {
	        var instantiatedPrefab = Instantiate (deathAnimation, transform.position, Quaternion.identity) as GameObject; //plug in deathanimation from enemy prefabs
	        instantiatedPrefab.transform.localScale = new Vector3(0.5f,0.5f,0.5f); //scale for the explosion
	    }
	    else {
	    	myAnimator.enabled = false;
	    	GetComponent<SpriteRenderer>().sprite = fallSprite;
	    	GetComponent<Collider2D>().enabled = false;
	    }
        if(falling) sound.clip = fallSound;
        else sound.clip = dieSound;
        sound.Play();
        myAnimator.SetBool("isHit", true);
        if(falling) yield return new WaitForSeconds(0.25f);
        else yield return null;
        Destroy(this.gameObject);
    }

    // public bool CheckInvTimer() {
    //     if (invTimer > 0) {
    //         invTimer--;
    //         return true;
    //     } else {
    //         invFrames = 1f / Time.deltaTime;
    //         alreadyHitting = false;
    //         return false;
    //     }
    // }

    protected bool CheckHoles(Direction d, float distance = 1.5f)
    {
        Ray2D r;
        if(d == Direction.Up) r = new Ray2D(transform.position, Vector2.up);
        else if(d == Direction.Down) r = new Ray2D(transform.position, Vector2.down);
        else if(d == Direction.Left) r = new Ray2D(transform.position, Vector2.left);
        else r = new Ray2D(transform.position, Vector2.right);
        Debug.DrawRay(r.origin, r.direction * distance, Color.blue);
        RaycastHit2D h = Physics2D.Raycast(r.origin, r.direction, distance);
        if(h.collider != null && h.collider.tag == "Fall") return true;
        else return false;
    } 

    // this function is hell !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public virtual void RandomizeDirection() 
    {
        //Debug.Log(":(");
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

    public float SetKnockbackTimer(float time) => knockbackDuration;
}
