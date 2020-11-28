using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shielding : MonoBehaviour
{
    
    public Animator linkAnimator;
    public AttackAndMove mainScript;
    Collider2D monster;
    public Rigidbody2D rb;
    float force = 10f;

    int knockbackspeed =  16;
    // Start is called before the first frame update
    void Start()
    {
        linkAnimator =GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKey(KeyCode.Z)){
            mainScript.shielding = true;
            linkAnimator.SetBool("shielding",true);
            mainScript.MoveSpeed = 5f;
        } 
         if(Input.GetKeyUp(KeyCode.Z)){
            mainScript.shielding = false;
            linkAnimator.SetBool("shielding",false);
        }
        if(mainScript.shielding == true){
            if(mainScript.facing == 1){
               upShieldRaycasts();
            } else if (mainScript.facing == 2){
               rightShieldRaycasts();
            } else if(mainScript.facing == 3){
               downShieldRaycasts();
            } else if(mainScript.facing==4){
               leftShieldRaycasts();
            }
        }
    }
    void upShieldRaycasts(){
        Ray2D shieldRay = new Ray2D (transform.position,transform.up);
        Ray2D shieldRayRight = new Ray2D (transform.position + new Vector3(0.5f,0f,0f),transform.up);
        Ray2D shieldRayLeft = new Ray2D (transform.position + new Vector3 (-0.5f,0f,0f),transform.up);
        float MaxShieldRayDist = 1.8f;
        Debug.DrawRay(shieldRay.origin, shieldRay.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayRight.origin,shieldRay.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayLeft.origin,shieldRay.direction * MaxShieldRayDist, Color.blue);
        RaycastHit2D ShieldRayHit = Physics2D.Raycast(shieldRay.origin, shieldRay.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayHitRight = Physics2D.Raycast(shieldRayRight.origin,shieldRayRight.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayHitLeft = Physics2D.Raycast(shieldRayLeft.origin, shieldRayLeft.direction,MaxShieldRayDist);
        if(ShieldRayHit.collider != null){
            if(ShieldRayHit.collider.tag == "Enemy"){
                monster = ShieldRayHit.collider;
                Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vector2FromMonsterToPlayer.Normalize();
                transform.Translate(vector2FromMonsterToPlayer * knockbackspeed * Time.deltaTime);
            }
        }
        if(ShieldRayHitRight.collider != null){
            if(ShieldRayHitRight.collider.tag == "Enemy"){
                monster = ShieldRayHitRight.collider;
                Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vector2FromMonsterToPlayer.Normalize();
                transform.Translate(vector2FromMonsterToPlayer * knockbackspeed * Time.deltaTime);
                Debug.Log("pls go2");
            }
        }
        if(ShieldRayHitLeft.collider != null){
            if(ShieldRayHitLeft.collider.tag == "Enemy"){
                monster = ShieldRayHitLeft.collider;
                Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vector2FromMonsterToPlayer.Normalize();
                transform.Translate(vector2FromMonsterToPlayer * knockbackspeed * Time.deltaTime);
                Debug.Log("pls go1");
            }
        }
    }
    void rightShieldRaycasts(){
        Ray2D shieldRayRight = new Ray2D (transform.position, transform.right);
        Ray2D shieldRayRightUp = new Ray2D (transform.position + new Vector3(0f, 0.5f,0f), transform.right);
        Ray2D shieldRayRightDown = new Ray2D (transform.position + new Vector3(0f, -0.5f,0f),transform.right);
        float MaxShieldRayDist = 1.8f;
        Debug.DrawRay(shieldRayRight.origin, shieldRayRight.direction * MaxShieldRayDist, Color.red);
        Debug.DrawRay(shieldRayRightUp.origin, shieldRayRightUp.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayRightDown.origin, shieldRayRightDown.direction * MaxShieldRayDist, Color.green);
        RaycastHit2D ShieldRayRightHit = Physics2D.Raycast(shieldRayRight.origin, shieldRayRight.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayRightUpHit = Physics2D.Raycast(shieldRayRightUp.origin, shieldRayRightUp.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayRightDownHit = Physics2D.Raycast(shieldRayRightDown.origin, shieldRayRightDown.direction,MaxShieldRayDist);
            if(ShieldRayRightHit.collider != null){
                if(ShieldRayRightHit.collider.tag == "Enemy"){
                    monster = ShieldRayRightHit.collider;
                    Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                    Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                    vectorFromMonsterTowardPlayer.Normalize();
                    rb.velocity += vector2FromMonsterToPlayer * force;
                }
            }
            if(ShieldRayRightUpHit.collider != null){
                if(ShieldRayRightUpHit.collider.tag == "Enemy"){
                    monster = ShieldRayRightUpHit.collider;
                    Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                    Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
                }
            } 
            if(ShieldRayRightDownHit.collider != null){
              if(ShieldRayRightDownHit.collider.tag == "Enemy"){
                    monster = ShieldRayRightDownHit.collider;
                    Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                    Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
                }
            }
    }
    void leftShieldRaycasts(){
        Ray2D shieldRayLeft = new Ray2D (transform.position, -transform.right);
        Ray2D shieldRayLeftUp = new Ray2D (transform.position + new Vector3(0f, 0.5f,0f), -transform.right);
        Ray2D shieldRayLeftDown = new Ray2D (transform.position + new Vector3(0f, -0.5f,0f),-transform.right);
        float MaxShieldRayDist = 1.8f;
        Debug.DrawRay(shieldRayLeft.origin, shieldRayLeft.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayLeftUp.origin, shieldRayLeftUp.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayLeftDown.origin, shieldRayLeftDown.direction * MaxShieldRayDist, Color.blue);
        RaycastHit2D ShieldRayLeftHit = Physics2D.Raycast(shieldRayLeft.origin, shieldRayLeft.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayLeftUpHit = Physics2D.Raycast(shieldRayLeftUp.origin, shieldRayLeftUp.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayLeftDownHit = Physics2D.Raycast(shieldRayLeftUp.origin, shieldRayLeftUp.direction,MaxShieldRayDist);
            if(ShieldRayLeftHit.collider != null){
                if(ShieldRayLeftHit.collider.tag == "Enemy"){
                        monster = ShieldRayLeftHit.collider;
                        Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                        Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
                }
            }
            if(ShieldRayLeftUpHit.collider != null){
                if(ShieldRayLeftUpHit.collider.tag == "Enemy"){
                    monster = ShieldRayLeftUpHit.collider;
                    Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                    Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
                }
            } 
            if(ShieldRayLeftDownHit.collider != null){
              if(ShieldRayLeftDownHit.collider.tag == "Enemy"){
                    monster = ShieldRayLeftDownHit.collider;
                    Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                    Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                    vectorFromMonsterTowardPlayer.Normalize();
                    rb.velocity += vector2FromMonsterToPlayer * force;
                }
            }
    }
    void downShieldRaycasts(){
        Ray2D shieldRayDown = new Ray2D (transform.position,-transform.up);
        Ray2D shieldRayDownRight = new Ray2D (transform.position + new Vector3(0.5f,0f,0f),-transform.up);
        Ray2D shieldRayDownLeft = new Ray2D (transform.position + new Vector3 (-0.5f,0f,0f),-transform.up);
        float MaxShieldRayDist = 1.8f;
        Debug.DrawRay(shieldRayDown.origin, shieldRayDown.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayDownRight.origin,shieldRayDownRight.direction * MaxShieldRayDist, Color.blue);
        Debug.DrawRay(shieldRayDownLeft.origin,shieldRayDownLeft.direction * MaxShieldRayDist, Color.blue);
        RaycastHit2D ShieldRayHitDown = Physics2D.Raycast(shieldRayDown.origin, shieldRayDown.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayHitDownRight = Physics2D.Raycast(shieldRayDownRight.origin,shieldRayDownRight.direction,MaxShieldRayDist);
        RaycastHit2D ShieldRayHitDownLeft = Physics2D.Raycast(shieldRayDownLeft.origin, shieldRayDownLeft.direction,MaxShieldRayDist);
        if(ShieldRayHitDown.collider != null){
            if(ShieldRayHitDown.collider.tag == "Enemy"){
                monster = ShieldRayHitDown.collider;
                Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
            }
        }
        if(ShieldRayHitDownRight.collider != null){
            if(ShieldRayHitDownRight.collider.tag == "Enemy"){
                monster = ShieldRayHitDownRight.collider;
                Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
            }
        }
        if(ShieldRayHitDownLeft.collider != null){
            if(ShieldRayHitDownLeft.collider.tag == "Enemy"){
                monster = ShieldRayHitDownLeft.collider;
                Vector3 vectorFromMonsterTowardPlayer = transform.position - monster.transform.position;
                Vector2 vector2FromMonsterToPlayer = new Vector2 (vectorFromMonsterTowardPlayer.x, vectorFromMonsterTowardPlayer.y);
                vectorFromMonsterTowardPlayer.Normalize();
                rb.velocity += vector2FromMonsterToPlayer * force;
            }
        }
    }
}

