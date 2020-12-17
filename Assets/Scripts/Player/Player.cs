using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Put this on Link. 
public class Player : MonoBehaviour
{
    ////////// PLAYER STATES //////////
    Idle idle;
    Walk walk;
    Hit hit;
    Shield shield;
    Jump jump;
    Push push;
    float health = 3;
    float knockbackTime = 1f;
    bool knockback;
    bool canKnockback = true;
    bool isKnockback;
    bool alive = true;
    bool fallingFirstFrame = false;
    int numberOfFallingsLinks = 0;
    public GameObject fallPrefab;
	float ResetTimer = 3f;

    public enum Direction {
        Up,
        Down,
        Left,
        Right,
        Static
    }
    public Direction currentDirection;
    public Vector2Int room;

    public RuntimeAnimatorController easterEggController;
    public string easterEggString;
    string easterEggInput;

    CameraMovement cam;
    public PlayerState currentState;
    bool moving; // True whenever movement keys are pressed

    public int keys;

    AudioSource sound;
    public AudioClip itemPickup, slash, fallSound;
    public Animator linkAnimator;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<CameraMovement>();

        idle = ScriptableObject.CreateInstance<Idle>();
        walk = ScriptableObject.CreateInstance<Walk>();
        hit = ScriptableObject.CreateInstance<Hit>();
        shield = ScriptableObject.CreateInstance<Shield>();
        jump = ScriptableObject.CreateInstance<Jump>();
        push = ScriptableObject.CreateInstance<Push>();

        idle.GrabComponents(this);
        walk.GrabComponents(this);
        hit.GrabComponents(this);
        shield.GrabComponents(this);
        jump.GrabComponents(this);

        push.GrabComponents(this);
    
        currentState = idle;
        room = new Vector2Int(0,0);

        keys = 0;

        easterEggInput = "";
    }

    void Update()
    {
        UpdateDirection();
        float old_x = transform.position.x;
        float old_y = transform.position.y;

        PlayerState oldState = currentState;

        if (oldState.canInterrupt) {
            // these if statements are honestly still hell lmao
            if (Input.GetKeyDown(KeyCode.X)) {            // ATTACK
                currentState = hit;
            } else if (Input.GetKeyDown(KeyCode.Space)) { // JUMP
                currentState = jump;
            } else if (Input.GetKeyDown(KeyCode.Z)) {     // SHIELD
                currentState = shield;
            } else if (moving && !oldState.isAction) {    // WALK
                currentState = walk;
            } else if (!moving && !oldState.isAction) {   // IDLE
                currentState = idle;
            }
            currentState.SetDirection(currentDirection);
            if (currentState == walk && currentState.CheckPush()) {
                currentState = push;                      // PUSH
                 }
            currentState.SetDirection(currentDirection);
            if (currentState != oldState) oldState.Reset();
        }
        
        if (moving) { currentState.linkAnimator.SetBool("walking", true); }
               else { currentState.linkAnimator.SetBool("walking", false); }
        
        if(isKnockback){
                knockbackTime -= Time.deltaTime;
            }
            //Debug.Log(knockbackTime);
            if(knockbackTime <= 0){
                knockbackTime = 1f;
                GetComponent<Animator>().SetBool("gothit",false);
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                isKnockback = false;
            }
  
        QuantizePosition();
        SwitchRoom(old_x, old_y);

        currentState.UpdateOnActive();
        foreach(char c in Input.inputString)
        {
            easterEggInput += c;
            if(easterEggInput.Length > easterEggString.Length) easterEggInput = "";
            if(easterEggInput != easterEggString.Substring(0, easterEggInput.Length)) easterEggInput = "";
            if(easterEggInput == easterEggString)
                GetComponent<Animator>().runtimeAnimatorController = easterEggController as RuntimeAnimatorController;
        }
    }

    void OnTriggerEnter2D(Collider2D activator){
        if(activator.tag == "Enemy"){
            Debug.Log("Enemy Hit");
            knockback = true;
            health -= 0.5f;
            Debug.Log(health);
            if(health > 0){
                if(canKnockback && !isKnockback){
                    Knockback();
                    GetComponent<Animator>().SetBool("gothit",true);
                    Vector2 fromMonsterToPlayer = new Vector2 (
                    transform.position.x - activator.transform.position.x,
                    transform.position.y - activator.transform.position.y);
                    fromMonsterToPlayer.Normalize();
                    GetComponent<Rigidbody2D>().velocity = fromMonsterToPlayer*5;
                    isKnockback = true;
                }
            }
        } else if (activator.tag == "Fall"){
            Fall();
        }
    }
    public void Knockback(){ }
         
    void Die(){ }
    void UpdateDirection() 
    {
        // move check if currentState is hit in here 
        Direction newDirection = Direction.Static;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) { // UP
            newDirection = Direction.Up; 
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { // LEFT
            newDirection = Direction.Left;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { // DOWN
            newDirection = Direction.Down;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { // RIGHT
            newDirection = Direction.Right;
        }

        moving = newDirection != Direction.Static;
        if (moving) currentDirection = newDirection;
    }

    // Rounds player's position onto the nearest tile.
    void QuantizePosition() 
    {
        float x = Mathf.Round(transform.position.x * 8) / 8;
        float y = Mathf.Round(transform.position.y * 8) / 8;
        this.transform.position = new Vector3 (x, y, 0f);
    }

    void SwitchRoom(float old_x, float old_y)
    {
        if(Mathf.Abs(transform.position.x % 20) == 10f) {
            if(old_x < transform.position.x) StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Right));
            else StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Left));
        }
        if(Mathf.Abs(transform.position.y % 16) == 9f) {
            if(old_y < transform.position.y) StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Up));
            else StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Down));
        }
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if(c.tag == "Chest" && Input.GetKey(KeyCode.X))
            c.GetComponent<Chest>().Open();
    }

////////////////////////////// GETTERS AND SETTERS //////////////////////////////
    public void SetIdle() => currentState = idle;

    // sets state to fall
    public void Fall()
    {
        fallingFirstFrame = true;
        if(fallingFirstFrame){
			linkAnimator.enabled = false;
            if(numberOfFallingsLinks == 0){
                if(currentDirection == Direction.Up){
                    Instantiate(fallPrefab, transform.position += new Vector3(0f,1.5f,0f) , Quaternion.identity);
                } else if(currentDirection == Direction.Right){
                    Instantiate(fallPrefab, transform.position += new Vector3(1.5f,0f,0f) , Quaternion.identity);
                }else if (currentDirection == Direction.Down){
                    Instantiate(fallPrefab, transform.position+= new Vector3(0f,-1.5f,0f) , Quaternion.identity);
                }else if (currentDirection == Direction.Left){
                    Instantiate(fallPrefab, transform.position+= new Vector3(-1.5f,0f,0f) , Quaternion.identity);
                }
                
                numberOfFallingsLinks +=1;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
		}
        //currentState = fall;
    }

    public int GetHealth() => (int)(health / 0.5);
}