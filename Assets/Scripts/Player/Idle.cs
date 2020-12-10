using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : PlayerState
{

    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    //public Idle(Player p) => GrabComponents(p);

    public override void UpdateOnActive()
    {
        linkAnimator.SetBool("walking", false);
    }
}
