using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerState
{
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR JUMP STATE
        linkAnimator = GetComponent<Animator>();
        
    }

    void Update()
    {
        
    }
}
