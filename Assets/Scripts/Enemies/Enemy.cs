using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //public float SwordHit; //General Hit Function

    void Start()
    {
        //Debug.Log("Hi, I'm an Enemy!");

    }

    void Update()
    {
        
    
    }

    public void SwordHit(){
        
        if(gameObject.name == "Gel"){
            Destroy(this.gameObject);
        }
        
        if(gameObject.name == "Stalfos"){
            GameObject EnemyStalfos = GameObject.Find("Stalfos");
            Stalfos StalfosScript = EnemyStalfos.GetComponent<Stalfos>();
            StalfosScript.StalfosHit = 1;
            //StalfosScript.StalfosHit = 0;
            
        }

        if(gameObject.name == "HardhatBeetle"){
            GameObject EnemyHardhatBeetle = GameObject.Find("HardhatBeetle");
            HardhatBeetle HardhatBeetleScript = EnemyHardhatBeetle.GetComponent<HardhatBeetle>();
            HardhatBeetleScript.Hit = 1;
            //HardhatBeetleScript.Hit = 0;
            
        }

        if(gameObject.name == "Moldorm" || gameObject.name == "Follow Moldorm"){
            GameObject EnemyMoldorm = GameObject.Find("Moldorm");
            Moldorm MoldormScript = EnemyMoldorm.GetComponent<Moldorm>();
            MoldormScript.Hit = 1;
            //StalfosScript.StalfosHit = 0;
            
        }

        if(gameObject.name == "Mini-Moldorm" || gameObject.name == "Follow Mini-Moldorm"){
            GameObject EnemyMiniMoldorm = GameObject.Find("Mini-Moldorm");
            Moldorm MiniMoldormScript = EnemyMiniMoldorm.GetComponent<Moldorm>();
            MiniMoldormScript.Hit = 1;
            //StalfosScript.StalfosHit = 0;
            
        }

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
