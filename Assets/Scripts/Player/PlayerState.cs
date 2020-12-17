using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : ScriptableObject
{
    protected Player player;
    protected float moveSpeed = 7f;
    protected Player.Direction direction; // WHICH WAY LINK IS FACING

    protected bool firstFrame = true; // BOOLEAN FLAG FOR Start() FUNCTIONALITY
    public bool canInterrupt = true;
    public bool isAction = false;

    // COMPONENTS
    public Animator linkAnimator;
    protected Transform playerTransform;
    protected Collider2D playerCollider;
    protected Rigidbody2D player_rb;
    protected AudioSource sound;
    protected AudioClip itemPickup, slash;

    ////////// PLACEHOLDER METHODS ////////// 
    public virtual void UpdateOnActive() { }
    public virtual void Reset() { }
    // Every state will have these methods, but the functionality will differ between states.
    // The methods are declared here so that UpdateOnActive() and Reset() can be called on any
    // object that inherits from PlayerState.

    public void GrabComponents(Player p) 
    {
        player = p;
        playerTransform = player.GetComponent<Transform>();
        linkAnimator = player.GetComponent<Animator>();
        playerCollider = player.GetComponent<Collider2D>();
        player_rb = player.GetComponent<Rigidbody2D>();
        sound = player.GetComponent<AudioSource>();
    }

    public bool CheckPush () {
    // Runs when an arrow key is pressed to check whether Link should be pushing or walking

        bool pushing = false;
        float pushRayLength = 1f;
        Ray2D pushCheckRay;

        if (direction == Player.Direction.Up || direction == Player.Direction.Down) {
            pushCheckRay = new Ray2D (playerTransform.position, GetVDirection());
        } else { 
            pushCheckRay = new Ray2D (playerTransform.position, GetHDirection());
        }
        Debug.DrawRay(pushCheckRay.origin, pushCheckRay.direction * pushRayLength, Color.green);
        RaycastHit2D pushRayHit = Physics2D.Raycast (
            pushCheckRay.origin, 
            pushCheckRay.direction, 
            pushRayLength);
        if (pushRayHit.collider != null && pushRayHit.collider.tag == "Wall") 
            pushing = true;
        
        linkAnimator.SetBool("pushing", pushing);
        return pushing;
    }

    // Called in UpdateOnActive() in any player state that allows movement.
    protected void Move() {

         if (Input.GetKey(KeyCode.UpArrow)    || Input.GetKey(KeyCode.W)) 
            playerTransform.Translate( 0,  Time.deltaTime * moveSpeed, 0);
         if (Input.GetKey(KeyCode.DownArrow)  || Input.GetKey(KeyCode.S)) 
            playerTransform.Translate( 0, -Time.deltaTime * moveSpeed, 0);
         if (Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.A)) 
            playerTransform.Translate(-Time.deltaTime * moveSpeed,  0, 0);
         if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            playerTransform.Translate( Time.deltaTime * moveSpeed,  0, 0);
    }

    // Turns Link's sprite––call this after Move() except in Hit
    public void Turn() {
        linkAnimator.SetFloat("AnimMoveX", Input.GetAxis("Horizontal"));
        linkAnimator.SetFloat("AnimMoveY", Input.GetAxis("Vertical"));
    }

    // GetVDirection() and GetHDirection() are for raycasts done by Shield() and Hit().
    // They return Vector3s that can be used to check for enemies in the correct direction
    // based on which way Link is facing.
    protected Vector3 GetVDirection () 
    {
        if (direction == Player.Direction.Down) return -playerTransform.up;
        return playerTransform.up;
    }

    protected Vector3 GetHDirection () 
    {
        switch (direction) {
            case (Player.Direction.Down):
                return -playerTransform.right;
            case (Player.Direction.Left):
                return -playerTransform.right;
            default:
                return playerTransform.right;
        }
    }
    public void SetDirection(Player.Direction d) => direction = d;
}