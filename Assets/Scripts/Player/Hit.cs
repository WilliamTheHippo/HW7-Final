using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : PlayerState
{
    const float CHARGETIMER = 0f;
    const float SPINTIMER = 0f;
    const float ATTACKTIMER = 0.3f;
    float charge = 0f;
    float spinTime = 0f;
    float attackTime = 0f;
    float pokeOffset = 0.4f;
    float swordRayLength = 1.8f;
    Vector3 vDirection;
    Vector3 hDirection;
    bool spinning = false;
    bool canPoke = true;
    bool poking = false;
    bool slashing  = false;
    bool firstFrame = true;

    // This is a constructor that passes through the player's Transform component so the 
    // states can use it.
    public Hit(Transform t) => playerTransform = t;

    ////////////////////////////// UTILITIES /////////////////////////////////
    void beginHit() {
        // not sure how animation controller works, but hopefully setting these once at the 
        // beginning of the hit will keep link facing the same direction, but let him move?
        linkAnimator.SetFloat("AnimMoveX", Input.GetAxis("Horizontal"));
        linkAnimator.SetFloat("AnimMoveY", Input.GetAxis("Vertical"));

        slashing = true;
        firstFrame = false;
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
        moveSpeed = 5f;

        // When the state is Hit, Player doesn't switch states in FixedUpdate() automatically to 
        // keep Link hitting when the arrow keys are pressed. 
        player.SetIdle(); // When the state is Hit, Player doesn't check state switching
    }

    ////////////////////////////// UPDATE /////////////////////////////////
    public void UpdateOnActive()
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

        if (attackTime >= ATTACKTIMER) {
            slashing = false;
            moveSpeed = 5f;
            attackTime = 0f;
        }

        if (charge >= CHARGETIMER && canPoke) {
            poking = true;
            canPoke = false;
        }

        if (slashing) Slash();
        if (poking) Poke();

        linkAnimator.SetBool("spinning", spinning);
        linkAnimator.SetBool("poking", poking);
        linkAnimator.SetBool("slashing", slashing);
    }

    ////////////////////////////// BEHAVIORS /////////////////////////////////
    void Slash() 
    {
        Vector3 vDirection = transform.up;
        Vector3 hDirection = transform.right;

        switch (direction) {
            case (Player.Direction.Down):
                vDirection = -transform.up;
                hDirection = -transform.right;
            break;
            case (Player.Direction.Left):
                hDirection = -transform.right;
            break;
        }

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
        if (vHit.collider != null && vHit.collider.tag == "Enemy")
            vHit.collider.GetComponent<Enemy>().SwordHit();
        if (hHit.collider != null && hHit.collider.tag == "Enemy")
            hHit.collider.GetComponent<Enemy>().SwordHit();

        moveSpeed = 0f;
        attackTime += Time.deltaTime;
    }

    void Poke() 
    {
        Ray2D pokeRay;

        switch (direction) {
            case (Player.Direction.Up):
                pokeRay = new Ray2D(transform.position + new Vector3(-pokeOffset, 0f, 0f), transform.up);
            break;
            case (Player.Direction.Down):
                pokeRay = new Ray2D(transform.position + new Vector3(pokeOffset, 0f, 0f), -transform.up);
            break;
            case (Player.Direction.Left):
                pokeRay = new Ray2D(transform.position + new Vector3(pokeOffset, 0f, 0f), -transform.right);
            break;
            default:
                pokeRay = new Ray2D(transform.position + new Vector3(0f, -pokeOffset, 0f), transform.right);
            break;
        }

        RaycastHit2D pokeRayHit = Physics2D.Raycast(pokeRay.origin, pokeRay.direction, swordRayLength);
        Debug.DrawRay(pokeRay.origin, pokeRay.direction * swordRayLength, Color.red);

        if (pokeRayHit.collider != null && pokeRayHit.collider.tag == "Enemy") SwordKnockback();
    }

    void SwordKnockback() 
    {
        Vector3 hitDestination = -transform.up * 10;
        Vector3 hitVector = hitDestination - transform.position;
        transform.position += hitVector.normalized;
        charge = 0f;
        poking = false;
    }
}
