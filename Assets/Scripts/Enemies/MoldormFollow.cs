using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormFollow : Enemy
{
    public Transform BallToFollow;
    public float LerpScale=5f;

    void Start()
    {
        //BallToFollow = transform.parent;
    }
    
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, BallToFollow.position, LerpScale * Time.deltaTime);
        GameObject EnemyMoldorm = GameObject.Find("Moldorm");
        Moldorm MoldormScript = EnemyMoldorm.GetComponent<Moldorm>();
        if(MoldormScript.Lives <= 0){  
            Destroy(this.gameObject);
        }
    }

    public override void SwordHit() {Debug.LogError("SwordHit() not implemented for " + gameObject.name + "!");}
}
