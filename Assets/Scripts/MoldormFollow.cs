using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormFollow : Enemy
{
    public Transform BallToFollow;
    public float LerpScale=1f;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, BallToFollow.position, LerpScale * Time.deltaTime);
    }


}
