using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : PlayerState
{
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR HIT STATE
        linkAnimator = GetComponent<Animator>();
        
    }

    void Update()
    {
        
    }
}
