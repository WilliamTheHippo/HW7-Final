using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{
    GameObject playerTransform;    

    void Start()
    {
        playerTransform = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        var playerVector = playerTransform.transform.position;
        Vector3 followVector = playerVector - transform.position;
        transform.position += followVector.normalized * Time.deltaTime;

        Debug.DrawLine( transform.position, playerVector, Color.yellow);
    }

    public override void SwordHit() {Debug.LogError("SwordHit() not implemented for " + gameObject.name + "!");}
}
