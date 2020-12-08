using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{
    GameObject playerTransform;    

    new void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player");
    }

    public override void OnUpdate()
    {
        var playerVector = playerTransform.transform.position;
        Vector3 followVector = playerVector - transform.position;
        transform.position += followVector.normalized * Time.deltaTime;

        Debug.DrawLine( transform.position, playerVector, Color.yellow);
    }

    public override void SwordHit() {
        Destroy(this.gameObject);
    }
}
