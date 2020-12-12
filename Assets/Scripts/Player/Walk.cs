using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : PlayerState
{
    public override void UpdateOnActive()
    {
        Move();
        Turn();
    }
}