using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{
    public void BeginIdle() 
    {
        linkAnimator.SetBool("pushing", false);
    }
    public override void UpdateOnActive()
    {
        if (firstFrame) BeginIdle();
    }
}
