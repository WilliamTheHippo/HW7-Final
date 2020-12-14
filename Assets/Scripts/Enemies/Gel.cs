using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{

    //Animator myAnimator;

    //private float isDead;  

    //public GameObject deathAnimation;

    //GameObject playerTransform;  
    void Start()
    {
        SetupEnemy();        
    }

    void FixedUpdate()
    {
        if(room != player.room) return;
        
        FollowPlayer();

            /*var playerVector = playerTransform.transform.position;
            Vector3 followVector = playerVector - transform.position;
            transform.position += followVector.normalized * Time.deltaTime;

            Debug.DrawLine( transform.position, playerVector, Color.yellow);*/
    }

    public override void SwordHit() {
        //isDead = 1;

        /*var instantiatedPrefab = Instantiate (deathAnimation, transform.position, Quaternion.identity) as GameObject;
        instantiatedPrefab.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Destroy(this.gameObject);*/

        //myAnimator.SetBool("isDead", true);
        //Destroy(this.gameObject, 0.7f);
    }
}