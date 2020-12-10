using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class for the objects that follow the head
public class MiniMoldormFollow : Enemy
{
    public Transform BallToFollow;
    public float LerpScale=5f;

    void Start()
    {
        AssignRoom();
        player = GameObject.Find("Player").GetComponent<Player>();
        //BallToFollow = transform.parent;
    }

    void FixedUpdate()
    {
        if(room != player.room) return;
        if(BallToFollow == null){
            Destroy(this.gameObject);
        }
        /*GameObject EnemyMoldorm = GameObject.Find("Mini-Moldorm");
        MiniMoldorm MoldormScript = EnemyMoldorm.GetComponent<MiniMoldorm>();
        if(MoldormScript.Lives <= 0){

            Destroy(this.gameObject);
        }*/
        
        transform.position = Vector3.Lerp(transform.position, BallToFollow.position, LerpScale * Time.deltaTime);

    
    }

    public override void SwordHit() {
        Debug.LogError("SwordHit() not implemented for " + gameObject.name + "!");
        base.SwordHit();
    }
}
