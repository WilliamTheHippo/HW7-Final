using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tmp_Camera : MonoBehaviour
{
	//just for the demo, not the full game
	enum Direction{Up, Down, Left, Right}
	Direction direction;
	Vector3 position;
	bool inputLock;

	void Start()
	{
		inputLock = false;
	}

	void FixedUpdate()
	{
		if(!inputLock)
		{
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				inputLock = true;
				position = transform.position + new Vector3(0f,16f,0f);
				direction = Direction.Up;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{
				inputLock = true;
				position = transform.position + new Vector3(0f,-16f,0f);
				direction = Direction.Down;
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				inputLock = true;
				position = transform.position + new Vector3(-20f,0f,0f);
				direction = Direction.Left;
			}
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				inputLock = true;
				position = transform.position + new Vector3(20f,0f,0f);
				direction = Direction.Right;
			}
		}
		else
		{
			if(transform.position == position)
			{
				inputLock = false;
				return;
			}
			if(direction == Direction.Up) transform.position += new Vector3(0f,0.5f,0f);
			if(direction == Direction.Down) transform.position += new Vector3(0f,-0.5f,0f);
			if(direction == Direction.Left) transform.position += new Vector3(-0.5f,0f,0f);
			if(direction == Direction.Right) transform.position += new Vector3(0.5f,0f,0f);

		}
	}
}
