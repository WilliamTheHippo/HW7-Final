using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public AttackAndMove player;
	public bool Panning {get; private set;}
	public enum Direction {Up, Down, Left, Right}

	public Room firstRoom;
	Room room;
	Room nextRoom;

	void Start()
	{
		room = firstRoom;
		Panning = false;
	}

	public IEnumerator MoveCamera(Direction direction)
	{
		Panning = true;
		//player.Idle();
		int times = direction == Direction.Left || direction == Direction.Right ? 40 : 32;
		Vector3 delta = new Vector3(0f,0f,0f);
		Vector2Int roomDelta = new Vector2Int(0,0);
		if(direction == Direction.Left)
		{
			delta = new Vector3(-0.5f,0f,0f);
			roomDelta = new Vector2Int(-1,0);
			nextRoom = room.left;
			player.transform.position += new Vector3(-0.125f,0f,0f);
		}
		if(direction == Direction.Right)
		{
			delta = new Vector3(0.5f,0f,0f);
			roomDelta = new Vector2Int(1,0);
			nextRoom = room.right;
			player.transform.position += new Vector3(0.125f,0f,0f);
		}
		if(direction == Direction.Up)
		{
			delta = new Vector3(0f,0.5f,0f);
			roomDelta = new Vector2Int(0,1);
			nextRoom = room.up;
			player.transform.position += new Vector3(0f,0.125f,0f);
		}
		if(direction == Direction.Down)
		{
			delta = new Vector3(0f,-0.5f,0f);
			roomDelta = new Vector2Int(0,-1);
			nextRoom = room.down;
			player.transform.position += new Vector3(0f,-0.125f,0f);
		}

		for(int i = 0; i < times; i++)
		{
			transform.position += delta;
			// if(player.onDoorTrigger && i % times / 8 == 0)
			// 	player.transform.position += delta / 1.25f;
			yield return new WaitForSeconds(0.01f);
		}
		player.room += roomDelta;
		nextRoom.Initialize();
		room = nextRoom;
		Panning = false;
		//player.onDoorTrigger = false;
	}
}
