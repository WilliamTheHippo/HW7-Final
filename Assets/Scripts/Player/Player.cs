using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public bool onDoorTrigger;

	[Header("Sprites")]
	public Sprite front;
	public Sprite back;
	public Sprite side;
	public Sprite[] walk;

	CameraMovement cam;
	enum Direction {Up, Down, Left, Right}
	Direction direction;
	SpriteRenderer sr;
	Sprite idle;

	bool walking;
	//bool shielding;

	bool coroutine_FlipX;
	bool coroutine_SideWalk;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		idle = sr.sprite;
		cam = Camera.main.GetComponent<CameraMovement>();
		onDoorTrigger = false;
		direction = Direction.Down;

		walking = false;
		//shielding = false;
	}

	void FixedUpdate()
	{
		if(cam.Panning) return;
		float old_x = transform.position.x;
		float old_y = transform.position.y;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			if(!walking) StartCoroutine("Walk");
			direction = Direction.Up;
			transform.position += new Vector3(0f, moveSpeed, 0f);
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			if(!walking) StartCoroutine("Walk");
			direction = Direction.Down;
			transform.position += new Vector3(0f, -moveSpeed, 0f);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			if(!walking) StartCoroutine("Walk");
			direction = Direction.Left;
			transform.position += new Vector3(-moveSpeed, 0f, 0f);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			if(!walking) StartCoroutine("Walk");
			direction = Direction.Right;
			transform.position += new Vector3(moveSpeed, 0f, 0f);
		}
		else Idle();
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

	public void Idle()
	{
		walking = false;
		StopCoroutine("Walk");
		if(direction == Direction.Up) sr.sprite = back;
		if(direction == Direction.Down) sr.sprite = front;
		if(direction == Direction.Left || direction == Direction.Right) sr.sprite = side;
		if(direction == Direction.Left) sr.flipX = true;
		if(direction == Direction.Right) sr.flipX = false;
	}

	IEnumerator Walk()
	{
		walking = true;
		int offset = 0;
		if(direction == Direction.Down) offset += 2;
		if(direction == Direction.Left || direction == Direction.Right) offset += 4;
		while(true)
		{
			sr.sprite = walk[offset];
			sr.sprite = walk[offset+1];
			yield return new WaitForSeconds(0.25f);
		}
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
