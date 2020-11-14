using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;

	enum CameraDirection{Up, Down, Left, Right}
	bool panning;

	void Start()
	{
		panning = false;
	}

	void FixedUpdate()
	{
		if(panning) return;
		float old_x = transform.position.x;
		float old_y = transform.position.y;
		if(Input.GetKey(KeyCode.UpArrow)) transform.position += new Vector3(0f, moveSpeed, 0f);
		if(Input.GetKey(KeyCode.DownArrow)) transform.position += new Vector3(0f, -moveSpeed, 0f);
		if(Input.GetKey(KeyCode.LeftArrow)) transform.position += new Vector3(-moveSpeed, 0f, 0f);
		if(Input.GetKey(KeyCode.RightArrow)) transform.position += new Vector3(moveSpeed, 0f, 0f);
		QuantizePosition();
		if(Mathf.Abs(transform.position.x % 20) == 10f)
		{
			if(old_x < transform.position.x) StartCoroutine(MoveCamera(CameraDirection.Right));
			else StartCoroutine(MoveCamera(CameraDirection.Left));
		}
		if(Mathf.Abs(transform.position.y % 16) == 9f)
		{
			if(old_y < transform.position.y) StartCoroutine(MoveCamera(CameraDirection.Up));
			else StartCoroutine(MoveCamera(CameraDirection.Down));
		}
	}

	IEnumerator MoveCamera(CameraDirection direction)
	{
		panning = true;
		int times = direction == CameraDirection.Left || direction == CameraDirection.Right ? 40 : 32;
		Vector3 delta = new Vector3(0f,0f,0f);
		if(direction == CameraDirection.Left)
		{
			delta = new Vector3(-0.5f,0f,0f);
			transform.position += new Vector3(-0.125f,0f,0f);
		}
		if(direction == CameraDirection.Right)
		{
			delta = new Vector3(0.5f,0f,0f);
			transform.position += new Vector3(0.125f,0f,0f);
		}
		if(direction == CameraDirection.Up)
		{
			delta = new Vector3(0f,0.5f,0f);
			transform.position += new Vector3(0f,0.125f,0f);
		}
		if(direction == CameraDirection.Down)
		{
			delta = new Vector3(0f,-0.5f,0f);
			transform.position += new Vector3(0f,-0.125f,0f);
		}

		for(int i = 0; i < times; i++)
		{
			Camera.main.transform.position += delta;
			yield return new WaitForSeconds(0.01f); //CHANGE THIS!
		}
		panning = false;
	}

	void QuantizePosition()
	{
		float x = Mathf.Round(transform.position.x * 8) / 8;
		float y = Mathf.Round(transform.position.y * 8) / 8;
		transform.position = new Vector3(x,y,0f); 
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Fall") Debug.Log("Falling!");
	}
}
