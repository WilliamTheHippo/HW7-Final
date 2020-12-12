using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    ////////// PLAYER STATES //////////
    Idle idle;
    Walk walk;
    Hit hit;
    Shield shield;
    Jump jump;
    Fall fall;
    Push push;

    public enum Direction {
        Up,
        Down,
        Left,
        Right,
        Static
    }
    public Direction currentDirection;
    public Vector2Int room;
    CameraMovement cam;
    PlayerState currentState;
    bool moving; // True whenever movement keys are pressed

    public AudioSource sound;
    public AudioClip itemPickup, slash;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<CameraMovement>();

        idle = ScriptableObject.CreateInstance<Idle>();
        walk = ScriptableObject.CreateInstance<Walk>();
        hit = ScriptableObject.CreateInstance<Hit>();
        shield = ScriptableObject.CreateInstance<Shield>();
        jump = ScriptableObject.CreateInstance<Jump>();
        fall = ScriptableObject.CreateInstance<Fall>();
        push = ScriptableObject.CreateInstance<Push>();

        idle.GrabComponents(this);
        walk.GrabComponents(this);
        hit.GrabComponents(this);
        shield.GrabComponents(this);
        jump.GrabComponents(this);
        fall.GrabComponents(this);
        push.GrabComponents(this);


        currentState = idle;
        room = new Vector2Int(0,0);
    }

    void FixedUpdate()
    {
        float old_x = transform.position.x;
        float old_y = transform.position.y;

        PlayerState oldState = currentState;
        
        UpdateDirection();

        // these if statements are honestly still hell lmao
        if (Input.GetKeyDown(KeyCode.X)) {            // ATTACK
            sound.clip = slash;
            sound.Play();
            currentState = hit;
            Debug.Log("attack");
        } else if (Input.GetKeyDown(KeyCode.Space)) { // JUMP
            currentState = jump;
            Debug.Log("jump");
        } else if (Input.GetKeyDown(KeyCode.Z)) {     // SHIELD
            currentState = shield;
            Debug.Log("shield");
        
        } else if (currentState == idle &&  moving) { // WALK
            currentState = walk;
            Debug.Log("walk");
        } else if (currentState == walk && !moving) { // IDLE
            currentState = idle;
            Debug.Log("idle");
        }
        if (currentState == walk && currentState.CheckPush()) {
            currentState = push;                      // PUSH
            Debug.Log("push"); }


        currentState.SetDirection(currentDirection);
        if (currentState != oldState){
            oldState.Reset();
        } 
        
        Debug.Log("updateOnActive");
        currentState.UpdateOnActive();
  
        QuantizePosition();
        SwitchRoom(old_x, old_y);
    }

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
        Debug.Log(newDirection);
    }

    // Rounds player's position onto the nearest tile.
    void QuantizePosition() 
    {
        float x = Mathf.Round(transform.position.x * 8) / 8;
        float y = Mathf.Round(transform.position.y * 8) / 8;
        transform.position = new Vector3 (x, y, 0f);
    }

    void SwitchRoom(float old_x, float old_y)
    {
        if(Mathf.Abs(transform.position.x % 20) == 10f)
        {
            if(old_x < transform.position.x) StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Right));
            else StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Left));
        }
        if(Mathf.Abs(transform.position.y % 16) == 9f)
        {
            if(old_y < transform.position.y) StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Up));
            else StartCoroutine(cam.MoveCamera(CameraMovement.Direction.Down));
        }
    }

////////////////////////////// GETTERS AND SETTERS //////////////////////////////
    public void SetIdle() => currentState = idle;
}