using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : ScriptableObject
{
    protected Player player;
    protected float moveSpeed = 5f;
    protected Player.Direction direction; // WHICH WAY LINK IS FACING

    protected bool firstFrame = true; // BOOLEAN FLAG FOR Start() FUNCTIONALITY

    // COMPONENTS
    protected Animator linkAnimator;
    protected Transform playerTransform;
    protected Collider2D playerCollider;
    protected Rigidbody2D player_rb;

    ////////// PLACEHOLDER METHODS ////////// 
    public virtual void UpdateOnActive() {
        Debug.Log("empty update");
     }
    public void Reset() { }
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
    }

    public bool CheckPush () {
    // Runs when an arrow key is pressed to check whether Link should be pushing or walking

        bool pushing = false;
        float pushRayLength = 1f;

        Ray2D pushCheckRay = new Ray2D (playerTransform.position, playerTransform.up);
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

         if (Input.GetKey(KeyCode.UpArrow)    || Input.GetKey(KeyCode.W)) {
            playerTransform.Translate( 0,  Time.deltaTime * moveSpeed, 0);
            Debug.Log("up");
        } if (Input.GetKey(KeyCode.DownArrow)  || Input.GetKey(KeyCode.S)) {
            playerTransform.Translate( 0, -Time.deltaTime * moveSpeed, 0);
            Debug.Log("down");
        } if (Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.A)) {
            playerTransform.Translate(-Time.deltaTime * moveSpeed,  0, 0);
            Debug.Log("left");
        } if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            playerTransform.Translate( Time.deltaTime * moveSpeed,  0, 0);
            Debug.Log("right"); }
    }

    // GetVDirection() and GetHDirection() are for raycasts done by Shield() and Hit().
    // They return Vector3s that can be used to check for enemies in the correct direction
    // based on which way Link is facing.
    // KEY:
    //  - UP    :  transform.up /  transform.right
    //  - DOWN  : -transform.up / -transform.right
    //  - LEFT  :  transform.up / -transform.right
    //  - RIGHT :  transform.up /  transform.right
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