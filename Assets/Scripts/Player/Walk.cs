using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerState
{
    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Walk(Transform t) => playerTransform = t;

    void Start() {
        
    }

    public void UpdateOnActive()
    {
        ///////// ANIMATOR STUFF //////////
        linkAnimator.SetBool("walking", true);
        linkAnimator.SetFloat("AnimMoveX", Input.GetAxis("Horizontal"));
        linkAnimator.SetFloat("AnimMoveY", Input.GetAxis("Vertical"));

        Move();
    }
}