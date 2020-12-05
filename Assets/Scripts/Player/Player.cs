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
        if (cam.Panning) return;

        // The if statements below are better but  honestly still hell and there's probably a more 
        // elegant way to do this... 
        if (currentState != hit) {

            if (Input.GetKeyDown(KeyCode.X)) { // ATTACK –– prioritized over all other actions
                currentState = hit;

            } else { 

                UpdateDirection();
                
                if (Input.GetKeyDown(KeyCode.Z)) { // SHIELD
                    currentState = shield;
                } 
                else if (Input.GetKeyDown(KeyCode.Space)) { // JUMP
                    currentState = jump;
                } else { // IDLE IF NO BUTTONS ARE PRESSED
                    currentState = idle;
                }
            }
        }
        ///////////// UPDATE CURRENT STATE VALUES //////////////
        currentState.setDirection(currentDirection);

        if (currentState == walk && currentState.CheckPush()) 
            currentState = push;

        currentState.UpdateOnActive();
    }

    void UpdateDirection() {
        Direction newDirection = Direction.Static;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) { // UP
            currentState = walk;
            newDirection = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) { // LEFT
            currentState = walk;
            newDirection = Direction.Left;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) { // DOWN
            currentState = walk;
            newDirection = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) { // RIGHT
            currentState = walk;
            newDirection = Direction.Right;
        }

        if (newDirection != Direction.Static) {
            currentDirection = newDirection;
            if (currentState != hit) currentState = walk;
        }
    }

    public void SetIdle() => currentState = idle;
}