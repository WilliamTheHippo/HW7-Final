using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Vector2Int room;

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

    Transform thisTransform;
    CameraMovement cam;


    PlayerState currentState;

    void Start()
    {
        thisTransform = this.GetComponent<Transform>();
        cam = Camera.main.GetComponent<CameraMovement>();

        idle = new Idle(thisTransform);
        walk = new Walk(thisTransform);
        hit = new Hit(thisTransform);
        shield = new Shield(thisTransform);
        jump = new Jump(thisTransform);
        fall = new Fall(thisTransform);
        push = new Push(thisTransform);

        currentState = idle;
    }

    void FixedUpdate()
    {
        float old_x = transform.position.x;
        float old_y = transform.position.y;
        
        // The if statements below are honestly still hell lmao

        // Hit and Jump can't be interrupted if other keys are pressed 
        if (currentState != hit && currentState != jump) {

            // ATTACK –– prioritized over all other actions
            if (Input.GetKeyDown(KeyCode.X)) { currentState = hit; } 

            else { 
                UpdateDirection();
                
                // SHIELD
                if (Input.GetKeyDown(KeyCode.Z)) { currentState = shield; }
                // JUMP
                else if (Input.GetKeyDown(KeyCode.Space)) { currentState = jump; } 
                // IDLE IF NO BUTTONS ARE PRESSED
                else { currentState = idle; }
            }
        }

        ///////////// UPDATE CURRENT STATE VALUES //////////////
        currentState.SetDirection(currentDirection);

        if (currentState == walk && currentState.CheckPush()) 
            currentState = push;

        currentState.UpdateOnActive();

        QuantizePosition();
        SwitchRoom(old_x, old_y);

    }

    void UpdateDirection() 
    {
        // move check if currentState is hit in here 
        Direction newDirection = Direction.Static;
        // maybe these should be GetKey?
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) { // UP
            //currentState = walk;
            newDirection = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) { // LEFT
            //currentState = walk;
            newDirection = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) { // DOWN
            //currentState = walk;
            newDirection = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) { // RIGHT
            //currentState = walk;
            newDirection = Direction.Right;
        }

        if (newDirection != Direction.Static) {
            currentDirection = newDirection;
            if (currentState != hit) currentState = walk;
        }
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