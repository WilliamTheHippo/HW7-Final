using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : Enemy
{
    GameObject playerTransform;  

    Animator myAnimator;

    private float isDead;  

    void Start()
    {
        playerTransform = GameObject.Find("Player");

        myAnimator = GetComponent<Animator>();

        isDead = 0;
    }

    void FixedUpdate()
    {
        if (isDead == 0){
            var playerVector = playerTransform.transform.position;
            Vector3 followVector = playerVector - transform.position;
            transform.position += followVector.normalized * Time.deltaTime;

            Debug.DrawLine( transform.position, playerVector, Color.yellow);
        }
    }

    public override void SwordHit() {
        isDead = 1;
        myAnimator.SetBool("isDead", true);
        Destroy(this.gameObject, 0.7f);
        //Destroy(this.gameObject);
    }
}
