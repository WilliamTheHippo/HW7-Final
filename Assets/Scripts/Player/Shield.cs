using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PlayerState
{
    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Shield(Transform t) => playerTransform = t;
}
