using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{

    public Animator linkAnimator;

    [HideInInspector] public float oldX;
    [HideInInspector] public float oldY;
    [HideInInspector] public float moveSpeed = 5f;
    public Player.Direction direction;
    public Transform playerTransform;

    float pushRayLength = 1f;
    
    public void UpdateOnActive() { }

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
        oldX = playerTransform.position.x;
        oldY = playerTransform.position.y;
    }

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