using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : PlayerState
{
    float chargeTimer = 0f;
    float spinTimer = 1f;
    float attackTimer = 0.3f;
    bool spinning = false;
    bool poking = false;
    bool slashing  = false;

    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Hit(Transform t) => playerTransform = t;
    void Start()
    {
        // PASS IN SPECIFIC ANIMATOR FOR HIT STATE
        linkAnimator = GetComponent<Animator>();
        
    }

    void UpdateOnActive()
    {
        chargeTimer += Time.deltaTime;

        Move();

        if (Input.GetKeyUp(KeyCode.X))
            keyRelease();
        
        if (spinning)
            spinTimer -= Time.deltaTime;
        
        if (spinTimer <= 0) {
            spinTimer = 0.5f;
            spinning = false;
        }
        // I can't find where attackTimer is decremented, so I'm not sure it ever runs?
        if (attackTimer <= 0) {
            slashing = false;
            
        }

        linkAnimator.SetBool("spinning", spinning);
        linkAnimator.SetBool("poking", poking);
        linkAnimator.SetBool("slashing", slashing);
    }

    void keyRelease() {
        if (chargeTimer >= 0.3f)
            spinning = true;
        chargeTimer = 0f;

        if (spinning) 
            spinTimer -= Time.deltaTime;
        if (spinTimer <= 0) {

        }
    }
}
