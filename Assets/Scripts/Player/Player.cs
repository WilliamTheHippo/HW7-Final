using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	//public bool onDoorTrigger;

	[Header("Sprites")]
	public Sprite front;
	public Sprite back;
	public Sprite side;
	public Sprite[] walk;
	int walk_offset;

	CameraMovement cam;
	enum Direction {Up, Down, Left, Right}
	Direction direction;
	SpriteRenderer sr;
	Sprite idle;

	bool walking;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		idle = sr.sprite;
		cam = Camera.main.GetComponent<CameraMovement>();
		direction = Direction.Down;
		walking = false;
	}

	void FixedUpdate()
	{
		if(cam.Panning) return;
		float old_x = transform.position.x;
		float old_y = transform.position.y;
		bool keyDown = false;
		walk_offset = 0;
		if(direction == Direction.Down) walk_offset += 2;
		if(direction == Direction.Left || direction == Direction.Right) walk_offset += 4;
		sr.flipX = direction == Direction.Left ? true : false;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			keyDown = true;
			direction = Direction.Up;
			if(!walking) StartCoroutine("Walk");
			walking = true;
			transform.position += new Vector3(0f, moveSpeed, 0f);
		}
		/*else*/ if(Input.GetKey(KeyCode.DownArrow))
		{
			keyDown = true;
			direction = Direction.Down;
			if(!walking) StartCoroutine("Walk");
			walking = true;
			transform.position += new Vector3(0f, -moveSpeed, 0f);
		}
		/*else*/ if(Input.GetKey(KeyCode.LeftArrow))
		{
			keyDown = true;
			direction = Direction.Left;
			if(!walking) StartCoroutine("Walk");
			walking = true;
			transform.position += new Vector3(-moveSpeed, 0f, 0f);
		}
		/*else*/ if(Input.GetKey(KeyCode.RightArrow))
		{
			keyDown = true;
			direction = Direction.Right;
			if(!walking) StartCoroutine("Walk");
			walking = true;
			transform.position += new Vector3(moveSpeed, 0f, 0f);
		}
		if(!keyDown) Idle();
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
		//sr.flipX = direction == Direction.Left ? true : false;
	}

	IEnumerator Walk() //can't just use FlipX anymore because shield sprites aren't mirrored
	{
		while(true)
		{
			sr.sprite = walk[walk_offset];
			yield return new WaitForSeconds(0.125f);
			sr.sprite = walk[walk_offset]; //duplicates improve game feel
			yield return new WaitForSeconds(0.125f);
			sr.sprite = walk[walk_offset+1];
			yield return new WaitForSeconds(0.125f);
			sr.sprite = walk[walk_offset+1];
		}
	}

	void Fall()
	{
		Debug.Log("Haven't implemented falling yet!");
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Fall") Fall();
	}
}
