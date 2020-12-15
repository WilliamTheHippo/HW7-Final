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

    public RuntimeAnimatorController easterEggController;
    public string easterEggString;
    string easterEggInput;

    CameraMovement cam;
    public PlayerState currentState;
    //PlayerState PlayerStateScript;
    bool moving; // True whenever movement keys are pressed

    AudioSource sound;
    public AudioClip itemPickup, slash;

    void Start()
    {
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

        easterEggInput = "";
    }

    void FixedUpdate()
    {
        float old_x = transform.position.x;
        float old_y = transform.position.y;

        PlayerState oldState = currentState;
        
        UpdateDirection();

        if (oldState.canInterrupt) {
            // these if statements are honestly still hell lmao
            if (Input.GetKeyDown(KeyCode.X)) {            // ATTACK
                currentState = hit;
            } else if (Input.GetKeyDown(KeyCode.Space)) { // JUMP
                currentState = jump;
            } else if (Input.GetKeyDown(KeyCode.Z)) {     // SHIELD
                currentState = shield;
            
            } else if (moving && !oldState.isAction) { // WALK
                currentState = walk;
            } else if (!moving && !oldState.isAction) { // IDLE
                currentState = idle;
            }
            if (currentState == walk && currentState.CheckPush()) {
                currentState = push;                      // PUSH
                 }


            currentState.SetDirection(currentDirection);
            if (currentState != oldState) oldState.Reset();
        }

        if (moving) { currentState.linkAnimator.SetBool("walking", true);  }
               else { currentState.linkAnimator.SetBool("walking", false); }

        // currentState.Turn(); 
        
        currentState.UpdateOnActive();
  
        QuantizePosition();
        //SwitchRoom(old_x, old_y);
    }

    void Update()
    {
        foreach(char c in Input.inputString)
        {
            easterEggInput += c;
            if(easterEggInput.Length > easterEggString.Length) easterEggInput = "";
            if(easterEggInput != easterEggString.Substring(0, easterEggInput.Length)) easterEggInput = "";
            if(easterEggInput == easterEggString)
                GetComponent<Animator>().runtimeAnimatorController = easterEggController as RuntimeAnimatorController;
        }
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
    }

    // Rounds player's position onto the nearest tile.
    void QuantizePosition() 
    {
        float x = Mathf.Round(transform.position.x * 8) / 8;
        float y = Mathf.Round(transform.position.y * 8) / 8;
        transform.position = new Vector3 (x, y, 0f);
    }

    /*void SwitchRoom(float old_x, float old_y)
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
    }*/

    void OnTriggerStay2D(Collider2D c)
    {
        if(c.tag == "Chest" && Input.GetKey(KeyCode.X))
            c.GetComponent<Chest>().Open();
    }

////////////////////////////// GETTERS AND SETTERS //////////////////////////////
    public void SetIdle() => currentState = idle;
}