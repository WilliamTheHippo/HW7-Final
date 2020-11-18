using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public Player player;
	public bool Panning {get; private set;}
	public enum Direction {Up, Down, Left, Right}

	void Start()
	{
		Panning = false;
	}

	public IEnumerator MoveCamera(Direction direction)
	{
		Panning = true;
		player.Idle();
		int times = direction == Direction.Left || direction == Direction.Right ? 40 : 32;
		Vector3 delta = new Vector3(0f,0f,0f);
		if(direction == Direction.Left)
		{
			delta = new Vector3(-0.5f,0f,0f);
			player.transform.position += new Vector3(-0.125f,0f,0f);
		}
		if(direction == Direction.Right)
		{
			delta = new Vector3(0.5f,0f,0f);
			player.transform.position += new Vector3(0.125f,0f,0f);
		}
		if(direction == Direction.Up)
		{
			delta = new Vector3(0f,0.5f,0f);
			player.transform.position += new Vector3(0f,0.125f,0f);
		}
		if(direction == Direction.Down)
		{
			delta = new Vector3(0f,-0.5f,0f);
			player.transform.position += new Vector3(0f,-0.125f,0f);
		}

		for(int i = 0; i < times; i++)
		{
			transform.position += delta;
			if(player.onDoorTrigger && i % times / 8 == 0)
				player.transform.position += delta / 1.25f;
			yield return new WaitForSeconds(0.01f);
		}
		Panning = false;
		player.onDoorTrigger = false;
	}
}
