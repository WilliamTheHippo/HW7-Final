using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Alex's code, meant to integrate with the tile system.
	//TODO Pull in Reef's attack code!
	public float moveSpeed;
	public bool onDoorTrigger;

	CameraMovement cam;

	void Start()
	{
		cam = Camera.main.GetComponent<CameraMovement>();
		onDoorTrigger = false;
	}

	void FixedUpdate()
	{
		if(cam.Panning) return;
		float old_x = transform.position.x;
		float old_y = transform.position.y;
		if(Input.GetKey(KeyCode.UpArrow)) transform.position += new Vector3(0f, moveSpeed, 0f);
		if(Input.GetKey(KeyCode.DownArrow)) transform.position += new Vector3(0f, -moveSpeed, 0f);
		if(Input.GetKey(KeyCode.LeftArrow)) transform.position += new Vector3(-moveSpeed, 0f, 0f);
		if(Input.GetKey(KeyCode.RightArrow)) transform.position += new Vector3(moveSpeed, 0f, 0f);
		QuantizePosition();
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

	void QuantizePosition()
	{
		float x = Mathf.Round(transform.position.x * 8) / 8;
		float y = Mathf.Round(transform.position.y * 8) / 8;
		transform.position = new Vector3(x,y,0f); 
	}

	void Fall()
	{
		Debug.Log("Haven't implemented falling yet!");
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Fall") Fall();
		if(c.tag == "DoorTrigger") onDoorTrigger = true;
	}
}
