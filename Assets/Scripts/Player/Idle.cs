using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{

    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Idle(Transform t) => playerTransform = t;
    
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR WALK STATE
        linkAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        linkAnimator.SetBool("walking", false);
    }
}
