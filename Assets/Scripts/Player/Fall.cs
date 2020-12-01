using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : PlayerState 
{
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR FALL STATE
        linkAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
