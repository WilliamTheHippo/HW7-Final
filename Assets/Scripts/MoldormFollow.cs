using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldormFollow : Enemy
{
    public Transform BallToFollow;
    public float LerpScale=1f;

    //public float Die;


    void Start()
    {
        //Die = 0;

    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, BallToFollow.position, LerpScale * Time.deltaTime);
    
        GameObject EnemyMoldorm = GameObject.Find("Moldorm");
        Moldorm MoldormScript = EnemyMoldorm.GetComponent<Moldorm>();
        if(MoldormScript.Lives <= 0){
                
            Destroy(this.gameObject);
        }

        /*if(Die == 1){
            Destroy(this.gameObject);
        }*/
    }


}
