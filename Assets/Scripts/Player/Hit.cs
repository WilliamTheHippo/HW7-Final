using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : PlayerState
{
    const float POKETIMER = 1.6f;
    const float SPINTIMER = 1f;
    const float SLASHTIMER = 0.8f;
    const float CHARGETIMER = 0.5f;  // 0.4f
    float charge = 0f;
    float spinTime = 0.5f;
    float attackTime = 0f;
    float pokeOffset = 0.4f;
    float swordRayLength = 1.8f;
    Vector3 vDirection;
    Vector3 hDirection;
    bool spinning = false;
    bool canPoke = true;
    bool poking = false;
    bool slashing  = false;

    // Slash :  initial swing
    // then he starts charging up
    // if you release the key and the charge is still held, he spins
    // otherwise he returns to idle

    ////////////////////////////// UTILITIES /////////////////////////////////
    void beginHit() {
        // not sure how animation controller works, but hopefully setting these once at the 
        // beginning of the hit will keep link facing the same direction, but let him move?
        //linkAnimator.SetFloat("AnimMoveX", Input.GetAxis("Horizontal"));
        //linkAnimator.SetFloat("AnimMoveY", Input.GetAxis("Vertical"));

        sound.clip = slash;
        sound.Play();

        firstFrame = false;
        canInterrupt = false;
        isAction = true;

    }

    public override void Reset() {
        spinning = poking = false;
        charge = attackTime = 0f;
        firstFrame = true;
        moveSpeed = 5f;
        spinTime = 0.5f;

        player.SetIdle();
        // When the state is Hit, Player doesn't switch states in FixedUpdate() automatically so 
        // Link continues to hit when the arrow keys are pressed.
    }

    ////////////////////////////// UPDATE /////////////////////////////////
    public override void UpdateOnActive()
    {
        attackTime += Time.deltaTime;
        if (firstFrame) beginHit();
        if (!slashing) Move();
        // SLASHTIMER = 0.8f;
        if (attackTime < SLASHTIMER) {
            slashing = true;
            moveSpeed = 5f;
        } else if (attackTime > SLASHTIMER) {
            slashing = false;
            poking = true;
            if (Input.GetKey(KeyCode.X)){
              charge += Time.deltaTime;
            } 
        } 
        if (attackTime >= SPINTIMER) {
            Debug.Log("go for it");
            if (Input.GetKeyUp(KeyCode.X)){
                if (charge >= CHARGETIMER){
                    spinning = true;
                    poking = true;
                    slashing = false;
                }
            }
        } else if(attackTime < SPINTIMER){
            if(Input.GetKeyUp(KeyCode.X)){
                Debug.Log("i wanna see this");
                Reset();
                slashing = false;
            }
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
        attackTime += Time.deltaTime;
    }

    
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

    void Spin()
    {   
        spinTime -= Time.deltaTime;
        if(spinTime< 0){
            Reset();
        }
    }

    void SwordKnockback() 
    {
        Vector3 hitDestination = -playerTransform.up * 10;
        Vector3 hitVector = hitDestination - playerTransform.position;
        playerTransform.position += hitVector.normalized;
        Reset();
    }
}
