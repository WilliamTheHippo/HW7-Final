using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{

    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Idle(Transform t) => playerTransform = t;

    void Update()
    {
        linkAnimator.SetBool("walking", false);
    }
}
