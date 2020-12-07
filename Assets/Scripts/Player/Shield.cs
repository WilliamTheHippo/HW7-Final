using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PlayerState
{
    float knockbackForce = 10f;

    float rayOffset = 0.5f;
    float maxRayDist = 1.8f;


    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Shield(Transform t) => playerTransform = t;

    void BeginShield() 
    {
        firstFrame = false;
        linkAnimator.SetBool("shielding", true);
        moveSpeed = 5f;
    }

    public void Reset() 
    {
        firstFrame = true;
        linkAnimator.SetBool("shielding", false);
        player.SetIdle();
    }

    public void UpdateOnActive() 
    {
        if (firstFrame)                 BeginShield();
        if (Input.GetKeyUp(KeyCode.Z))  Reset();

        Vector3 vDirection = GetVDirection(); // update raycast directions based on
        Vector3 hDirection = GetHDirection(); // which way Link is facing
        Ray2D cRay, rRay, lRay; // Raycast CENTER, RIGHT, and LEFT from where Link is facing

        //////////////////////////// RAYCAST //////////////////////////////
        if (direction == Player.Direction.Up || direction == Player.Direction.Down) {
            cRay = new Ray2D (playerTransform.position,  vDirection);
            rRay = new Ray2D (playerTransform.position + new Vector3( rayOffset, 0f, 0f), vDirection);
            lRay = new Ray2D (playerTransform.position + new Vector3(-rayOffset, 0f, 0f), vDirection);
        } else { // RIGHT OR LEFT
            cRay = new Ray2D (playerTransform.position,  hDirection);
            rRay = new Ray2D (playerTransform.position + new Vector3(0f,  rayOffset, 0f), hDirection);
            lRay = new Ray2D (playerTransform.position + new Vector3(0f, -rayOffset, 0f), hDirection);
        }
        Debug.DrawRay(cRay.origin, cRay.direction * maxRayDist, Color.blue);
        Debug.DrawRay(rRay.origin, rRay.direction * maxRayDist, Color.blue);
        Debug.DrawRay(lRay.origin, lRay.direction * maxRayDist, Color.blue);

        RaycastHit2D cRayHit = Physics2D.Raycast(cRay.origin, cRay.direction, maxRayDist);
        RaycastHit2D rRayHit = Physics2D.Raycast(rRay.origin, rRay.direction, maxRayDist);
        RaycastHit2D lRayHit = Physics2D.Raycast(lRay.origin, lRay.direction, maxRayDist);

        //////////////////////////// COLLISION //////////////////////////////
        if      (cRayHit.collider != null && cRayHit.collider.tag == "Enemy") { BounceOff(cRayHit.collider); }
        else if (rRayHit.collider != null && rRayHit.collider.tag == "Enemy") { BounceOff(rRayHit.collider); }
        else if (lRayHit.collider != null && lRayHit.collider.tag == "Enemy") { BounceOff(lRayHit.collider); }

        else { Move(); } // If Link isn't being knocked back, check arrow key movement
    }

    void BounceOff(Collider2D monster) {

        Vector2 fromMonsterToPlayer = new Vector2 (
            playerTransform.position.x - monster.transform.position.x,
            playerTransform.position.y - monster.transform.position.y);
        
        fromMonsterToPlayer.Normalize();
        player_rb.velocity = fromMonsterToPlayer * knockbackForce;
    }
}