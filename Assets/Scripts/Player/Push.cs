using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : PlayerState
{
    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Push(Transform t) => playerTransform = t;

    void BeginPush()
    {
        firstFrame = false;
    }

    void UpdateOnActive() 
    {
        if (firstFrame) BeginPush();
    }
}
