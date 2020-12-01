using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR WALK STATE
        linkAnimator = GetComponent<Animator>();

        currentDirection = Direction.Down;
    }

    void Update()
    {
        
    }
}
