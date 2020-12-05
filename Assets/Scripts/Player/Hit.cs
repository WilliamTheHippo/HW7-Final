using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : PlayerState
{
    const float CHARGETIMER = 0f;
    const float SPINTIMER = 1f;
    const float ATTACKTIMER = 0.3f;
    float charge = 0f;
    float spinTime = 0f;
    float attackTime = 0f;
    bool spinning = false;
    bool canPoke = true;
    bool poking = false;
    bool slashing  = false;
    bool firstFrame = true;

    // This is a constructor that passes through the Player Transform component so the 
    // states can use it.
    public Hit(Transform t) => playerTransform = t;

    //////////////////////////// UTILITY FUNCTIONS ////////////////////////////
    void beginHit() {
        // not sure how animation controller works, but hopefully setting these once at the 
        // beginning of the hit will keep link facing the same direction, but let him move?
        linkAnimator.SetFloat("AnimMoveX", Input.GetAxis("Horizontal"));
        linkAnimator.SetFloat("AnimMoveY", Input.GetAxis("Vertical"));

        slashing = true;
    }

    void keyRelease() {
        if (charge >= CHARGETIMER) spinning = true;
        if (!canPoke) canPoke = true;

        charge = 0f;
    }

    void Reset() {
        spinning = poking = slashing = false;
        charge = spinTime = attackTime = 0f;
        firstFrame = true;

        // When the state is Hit, Player doesn't switch states in FixedUpdate() automatically to 
        // keep Link hitting when the arrow keys are pressed. 
        player.SetIdle(); // When the state is Hit, Player doesn't check state switching
    }

    ////////////////////////////// UPDATE /////////////////////////////////
    void UpdateOnActive()
    {
        if (firstFrame) beginHit();

        Move();

        charge += Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.X)) keyRelease();
        
        if (spinning) spinTime += Time.deltaTime;
        
        if (spinTime >= SPINTIMER) {
            spinTime = 0f;
            spinning = false;
        }
        // I can't find where attackTime is decremented in the original code, so I'm not 
        // sure this ever runs?
        if (attackTime >= ATTACKTIMER) {
            slashing = false;
            moveSpeed = 5f;
            attackTime = 0f;
        }

        if (charge >= CHARGETIMER && canPoke) {
            poking = true;
            canPoke = false;
        }

        linkAnimator.SetBool("spinning", spinning);
        linkAnimator.SetBool("poking", poking);
        linkAnimator.SetBool("slashing", slashing);
    }

    ////////////////////////////// BEHAVIOR /////////////////////////////////

    void Slash() {

    }

    void Poke() {

    }

    
}
