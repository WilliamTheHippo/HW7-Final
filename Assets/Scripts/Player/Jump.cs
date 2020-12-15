using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerState
{
    const float JUMPTIMER = 0.25f;
    const float JUMPSPEED = 0.1f;
    float jumpTime = 0f;
    float jumpDirection = 1f;

    public void BeginJump() 
    {
        firstFrame = false;
        playerCollider.enabled = false;
        linkAnimator.SetBool("jumping", true);
        canInterrupt = false;
        isAction = true;
    }

    public override void UpdateOnActive()
    {
        if (firstFrame) {
            BeginJump();
            Debug.Log("first frame is true");
        }
        //Move();

        if (jumpTime >= JUMPTIMER) {
            if (jumpDirection == 1) {
                jumpDirection = -1;
            } else {
                Reset();
                return;
            }
        }

        jumpTime += Time.deltaTime;
        playerTransform.position += new Vector3 (0f, JUMPSPEED * jumpDirection, 0f);
        
    }

    public override void Reset() 
    {   
        player.SetIdle();
        jumpDirection = 1;
        linkAnimator.SetBool("jumping", false);
        playerCollider.enabled = true;
        firstFrame = true;
        jumpTime = 0f;
    }
}
