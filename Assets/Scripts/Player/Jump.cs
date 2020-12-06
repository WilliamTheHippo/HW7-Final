using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerState
{
    const float JUMPTIMER = 0.25f;
    const float JUMPSPEED = 0.1f;
    float jumpTime = 0f;
    float jumpDirection = 0;

    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Jump(Transform t) => playerTransform = t;

    public void BeginJump() 
    {
        jumpDirection = 1; 
        playerCollider.enabled = false;
        linkAnimator.SetBool("jumping", true);
    }

    public void UpdateOnActive()
    {
        if (jumpDirection == 0) BeginJump();

        Move();

        if (jumpTime >= JUMPTIMER) {
            if (jumpDirection == 1) {
                jumpDirection = -1;
            } else {
                Reset();
                return;
            }
        }

        jumpTime += Time.deltaTime;
        transform.position += new Vector3 (0f, JUMPSPEED * jumpDirection, 0f);
    }

    public void Reset() 
    {
        jumpDirection = 0;
        linkAnimator.SetBool("jumping", false);
        playerCollider.enabled = true;
        player.SetIdle();
    }

}
