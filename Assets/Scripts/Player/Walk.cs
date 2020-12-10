using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerState
{
    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.

    public override void UpdateOnActive()
    {
        Debug.Log("Walk update");

        ///////// ANIMATOR STUFF //////////
        linkAnimator.SetBool("walking", true);
        linkAnimator.SetFloat("AnimMoveX", Input.GetAxis("Horizontal"));
        linkAnimator.SetFloat("AnimMoveY", Input.GetAxis("Vertical"));
        Move();
    }
}