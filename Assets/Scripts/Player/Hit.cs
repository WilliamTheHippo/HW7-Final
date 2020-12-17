using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : PlayerState
{
    const float POKETIMER = 1.25f;
    const float SPINTIMER = 0.5f;
    const float SLASHTIMER = 0.25f;
    const float CHARGETIMER = 0.5f; 
    const float SPINDURATION = 0.25f;
    float charge = 0f;
    float spinTime = 0f;
    float attackTime = 0f;
    float pokeOffset = 0.4f;
    float swordRayLength = 1.8f;
    float swordSpinRayLength = 2.5f;
    Vector3 vDirection;
    Vector3 hDirection;
    bool spinning = false;
    bool canPoke = true;
    bool poking = false;
    bool slashing  = false;
    bool slashed = false;

    enum HitState {
        Slash,
        Poke,
        Spin, 
        Bye
    }
    HitState currentHitState = HitState.Slash;

    // Slash :  initial swing
    // then he starts charging up
    // if you release the key and the charge is still held, he spins
    // otherwise he returns to idle

    ////////////////////////////// UTILITIES /////////////////////////////////
    void BeginHit() 
    {
        currentHitState = HitState.Slash;
        sound.clip = player.slash;
        sound.Play();

        firstFrame = false;
        canInterrupt = false;
        isAction = true;
    }

    public override void Reset() 
    {
        spinning = poking = slashing = false;
        currentHitState = HitState.Bye;
        charge = attackTime = spinTime = 0f;
        firstFrame = true;
        moveSpeed = 7f;

        player.SetIdle();
        // When the state is Hit, Player doesn't switch states in FixedUpdate() automatically so 
        // Link continues to hit when the arrow keys are pressed.
    }

    ////////////////////////////// UPDATE /////////////////////////////////
    public override void UpdateOnActive()
    {
        attackTime += Time.deltaTime;
        if (firstFrame) BeginHit();

        switch (currentHitState) {
            case (HitState.Slash):
                moveSpeed = 5f;
                slashing = true;
                if (attackTime > SLASHTIMER) {
                    currentHitState = HitState.Poke;
                } 
            break;

            case (HitState.Poke):
                slashing = false;
                poking = true;
                Move();
                if (attackTime >= SPINTIMER && charge >= CHARGETIMER && Input.GetKeyUp(KeyCode.X)) {
                    linkAnimator.Play("spinning");
                    currentHitState = HitState.Spin;
                    //;
                } 
                else if (!Input.GetKey(KeyCode.X)) {
                    Reset();
                } else {
                    charge += Time.deltaTime;
                }
            break;

            case (HitState.Spin):
                poking = false;
                spinning = true;
                spinTime += Time.deltaTime;
                if (spinTime > SPINDURATION) Reset();
            break;

            default:
                Reset();
            break;
        }

        if (slashing) Slash();
        if (poking)   Poke();
        if (spinning) Spin();
        
        linkAnimator.SetBool("spinning", spinning);
        linkAnimator.SetBool("poking",   poking);
        linkAnimator.SetBool("slashing", slashing);
    }

    ////////////////////////////// BEHAVIORS /////////////////////////////////

    // The initial swing
    void Slash() 
    {
        Vector3 vDirection = GetVDirection(); // update raycast directions based on
        Vector3 hDirection = GetHDirection(); // which way Link is facing

        // Draw 3 rays: vertical, diagonal, and horizontal. 
        // collision is never checked on diagonal ray?
        Ray2D vRay = new Ray2D(playerTransform.position, vDirection);
        Ray2D dRay = new Ray2D(playerTransform.position, vDirection + hDirection);
        Ray2D hRay = new Ray2D(playerTransform.position, hDirection);

        Debug.DrawRay(vRay.origin, vRay.direction * swordRayLength, Color.green);
        Debug.DrawRay(dRay.origin, dRay.direction * swordRayLength, Color.green);
        Debug.DrawRay(hRay.origin, hRay.direction * swordRayLength, Color.green);

        RaycastHit2D vHit = Physics2D.Raycast(vRay.origin, vRay.direction, swordRayLength);
        RaycastHit2D hHit = Physics2D.Raycast(hRay.origin, hRay.direction, swordRayLength);

        // Check collision––if Link hit an enemy, run that enemy's SwordHit(). 
        if (vHit.collider != null && vHit.collider.tag == "Enemy"){
            vHit.collider.GetComponent<Enemy>().SwordHit();
        }

        if (hHit.collider != null && hHit.collider.tag == "Enemy"){
            hHit.collider.GetComponent<Enemy>().SwordHit();
        }
        moveSpeed = 0f;
    }

    // The charge
    void Poke() 
    {
        Ray2D pokeRay;
        moveSpeed = 5f;
        switch (direction) {
            case (Player.Direction.Up):
                pokeRay = new Ray2D(playerTransform.position + new Vector3(-pokeOffset, 0f, 0f),  playerTransform.up);
            break;
            case (Player.Direction.Down):
                pokeRay = new Ray2D(playerTransform.position + new Vector3( pokeOffset, 0f, 0f), -playerTransform.up);
            break;
            case (Player.Direction.Left):
                pokeRay = new Ray2D(playerTransform.position + new Vector3( pokeOffset, 0f, 0f), -playerTransform.right);
            break;
            default:
                pokeRay = new Ray2D(playerTransform.position + new Vector3(0f, -pokeOffset, 0f),  playerTransform.right);
            break;
        }

        RaycastHit2D pokeRayHit = Physics2D.Raycast(pokeRay.origin, pokeRay.direction, swordRayLength);
        Debug.DrawRay(pokeRay.origin, pokeRay.direction * swordRayLength, Color.red);

        if (pokeRayHit.collider != null && pokeRayHit.collider.tag == "Enemy") SwordKnockback();
    }

    // The spin attack
    void Spin()
    {   
        Vector3 vDirection = GetVDirection(); 
        Vector3 hDirection = GetHDirection();

        spinning = true;

        Ray2D upRay = new Ray2D(playerTransform.position, vDirection);
        Ray2D upRightRay = new Ray2D(playerTransform.position, vDirection + hDirection);
        Ray2D RightRay = new Ray2D(playerTransform.position, hDirection);
        Ray2D downRightRay = new Ray2D(playerTransform.position, -vDirection + hDirection);
        Ray2D downRay = new Ray2D(playerTransform.position, -vDirection);
        Ray2D downLeftRay = new Ray2D(playerTransform.position, -vDirection - hDirection);
        Ray2D leftRay = new Ray2D(playerTransform.position, -hDirection);
        Ray2D upLeftRay = new Ray2D(playerTransform.position, - hDirection + vDirection);
        RaycastHit2D upHit = Physics2D.Raycast(upRay.origin, upRay.direction, swordSpinRayLength);
        RaycastHit2D upRightHit = Physics2D.Raycast(upRightRay.origin, upRightRay.direction, swordSpinRayLength);
        RaycastHit2D RightHit = Physics2D.Raycast(RightRay.origin, RightRay.direction, swordSpinRayLength);
        RaycastHit2D downRightHit = Physics2D.Raycast(downRightRay.origin, downRightRay.direction, swordSpinRayLength);
        RaycastHit2D downHit = Physics2D.Raycast(downRay.origin, downRay.direction, swordSpinRayLength);
        RaycastHit2D downLeftHit = Physics2D.Raycast(downLeftRay.origin, downLeftRay.direction, swordSpinRayLength);
        RaycastHit2D leftHit = Physics2D.Raycast(leftRay.origin, leftRay.direction, swordSpinRayLength);
        RaycastHit2D upLeftHit = Physics2D.Raycast(upLeftRay.origin, upLeftRay.direction, swordSpinRayLength);
        Debug.DrawRay(upRay.origin, upRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(upRightRay.origin, upRightRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(RightRay.origin, RightRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(downRightRay.origin, downRightRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(downRay.origin, downRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(downLeftRay.origin, downLeftRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(leftRay.origin, leftRay.direction * swordSpinRayLength, Color.red);
        Debug.DrawRay(upLeftRay.origin, upLeftRay.direction * swordSpinRayLength, Color.red);
        Debug.Log("yo");
        if (upHit.collider != null && upHit.collider.tag == "Enemy") {
            upHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (upRightHit.collider != null && upRightHit.collider.tag == "Enemy"){
            upRightHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (RightHit.collider != null && RightHit.collider.tag == "Enemy"){
            RightHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (downRightHit.collider != null && downRightHit.collider.tag == "Enemy"){
            downRightHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (downHit.collider != null && downHit.collider.tag == "Enemy"){
            downHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (downLeftHit.collider != null && downLeftHit.collider.tag == "Enemy"){
            upHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (leftHit.collider != null && leftHit.collider.tag == "Enemy"){
            upHit.collider.GetComponent<Enemy>().SwordHit();
        }
        if (upLeftHit.collider != null && upLeftHit.collider.tag == "Enemy") 
            upLeftHit.collider.GetComponent<Enemy>().SwordHit();

    }

    void SwordKnockback() 
    {
        Vector3 hitDestination = -playerTransform.up * 10;
        Vector3 hitVector = hitDestination - playerTransform.position;
        playerTransform.position += hitVector.normalized ;
        Reset();
    }
}
