using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : PlayerState
{
    Player.Direction pushDirection;

    void BeginPush()
    {
        firstFrame = false;
        pushDirection = direction;
    }

    public override void UpdateOnActive() 
    {
        if (firstFrame) BeginPush();
        if (direction != pushDirection) player.SetIdle();
    }
}
