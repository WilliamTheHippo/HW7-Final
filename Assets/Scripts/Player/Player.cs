using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    PlayerState currentState;

    ////////// PLAYER STATES //////////
    Idle idle = new Idle();
    Walk walk = new Walk();
    Hit hit = new Hit();
    Shield shield = new Shield();
    Jump jump = new Jump();
    Fall falling = new Fall();

    public float moveSpeed = 5f;

    public enum Direction {
        Up,
        Down,
        Left,
        Right,
    }
    public Direction currentDirection;

    void Start()
    {
        currentState = idle;
    }

    void Update()
    {
        if (cam.Panning) return;

        if (Input.GetKeyDown(KeyCode.X)) {
            currentState = hit;
        } 
        else if (Input.GetKeyDown(KeyCode.Z)) {
            currentState = shield;
        } 
        else if (Input.GetKeyDown(KeyCode.Space)) {
            currentState = jump;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            currentState = walk;
            currentDirection = Direction.Up;
        }
        
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            currentState = walk;
            currentDirection = Direction.Left;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            currentState = walK;
            currentDirection = Direction.Down;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {  
            currentState = walk;
            currentDirection = Direction.Right;
        }

        currentState.UpdateOnActive();
    }
}