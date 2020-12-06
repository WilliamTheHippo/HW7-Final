using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{

    float moveSpeed = 5f;
    Player.Direction direction;
    Player player;
    Animator linkAnimator;
    protected Transform playerTransform;
    Collider2D playerCollider;
    Rigidbody2D player_rb;

    bool canStandStill;

    float pushRayLength = 1f;
    bool firstFrame = true;

    
    ////////// PLACEHOLDER METHODS ////////// 
    public void UpdateOnActive() { }
    public void Reset() { }
    // Every state will have these methods, but the functionality will differ between states.
    // The methods are declared here so that UpdateOnActive() and Reset() can be called on any
    // object that inherits from PlayerState.

    // all the child classes should run this Start() function unless they have their own?
    void Start() { 
        player = playerTransform.GetComponent<Player>();
        linkAnimator = player.GetComponent<Animator>();
        playerCollider = player.GetComponent<Collider2D>();
        player_rb = player.GetComponent<Rigidbody2D>();
    }

    public bool CheckPush () {
    // Runs when an arrow key is pressed to check whether Link should be pushing or walking

        bool pushing = false;

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

    public void setDirection(Player.Direction d) {
        direction = d;
    }

    // Called in UpdateOnActive() in any player state that allows movement.
    public void Move() {

        switch (direction) {
            case (Player.Direction.Up):
                playerTransform.Translate(0, -Time.deltaTime, 0);
            break;
            case (Player.Direction.Down):
                playerTransform.Translate(0, Time.deltaTime, 0);
            break;
            case (Player.Direction.Left):
                playerTransform.Translate(Time.deltaTime, 0, 0);
            break;
            case (Player.Direction.Right):
                playerTransform.Translate(-Time.deltaTime, 0, 0);
            break;
        }
    }
}