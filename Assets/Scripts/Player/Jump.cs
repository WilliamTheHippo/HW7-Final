using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : PlayerState
{
    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Jump(Transform t) => playerTransform = t;
}
