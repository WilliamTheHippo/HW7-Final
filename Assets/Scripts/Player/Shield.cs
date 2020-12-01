using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PlayerState
{
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR SHIELD STATE
        linkAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
