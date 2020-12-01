using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerState
{
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR WALK STATE
        linkAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
