using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    PlayerState currentState;

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
    }
    public Direction currentDirection;

    Transform thisTransform;
    CameraMovement cam;

    void Start()
    {
        thisTransform = this.GetComponent<Transform>();

        idle = new Idle(thisTransform);
        walk = new Walk(thisTransform);
        hit = new Hit(thisTransform);
        shield = new Shield(thisTransform);
        jump = new Jump(thisTransform);
        fall = new Fall(thisTransform);
        push = new Push(thisTransform);

        currentState = idle;
    }

    void Update()
    {
        if (cam.Panning) return;
        

        // these if statements are honestly still hell and there's probably a more elegant
        // way of doing this ... 
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) { // UP
            currentState = walk;
            currentDirection = Direction.Up;
        }
        
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) { // LEFT
            currentState = walk;
            currentDirection = Direction.Left;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) { // DOWN
            currentState = walk;
            currentDirection = Direction.Down;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) { // RIGHT
            currentState = walk;
            currentDirection = Direction.Right;
        }

        if (Input.GetKeyDown(KeyCode.X)) { // ATTACK
            currentState = hit;
        } 
        else if (Input.GetKeyDown(KeyCode.Z)) { // SHIELD
            currentState = shield;
        } 
        else if (Input.GetKeyDown(KeyCode.Space)) { // JUMP
            currentState = jump;
        } 

        // bug: the way it's currently written, this will set it to idle on every frame
        else { // IDLE IF NO BUTTONS ARE PRESSED
            currentState = idle;
        }

        ///////////// UPDATE CURRENT STATE VALUES //////////////
        currentState.setDirection(currentDirection);

        if (currentState == walk && currentState.CheckPush()) 
            currentState = push;

        currentState.UpdateOnActive();
    }
}