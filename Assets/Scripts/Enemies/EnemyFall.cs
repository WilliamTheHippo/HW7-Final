using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFall : MonoBehaviour
{
    public void SwordHit(){
        //some enemies only get knockback, others get destroyed, etc.
    }

    public void Fall()
    {
        Debug.Log("Haven't implemented falling yet!");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Fall") Fall();
    }
}
