using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	public bool onDoorTrigger;

	public Sprite front;
	public Sprite back;
	public Sprite[] walkAnimation;

	CameraMovement cam;
	enum Direction {Up, Down, Left, Right}
	Direction direction;
	SpriteRenderer sr;
	Sprite idle;

	bool coroutine_FlipX;
	bool coroutine_SideWalk;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		idle = sr.sprite;
		cam = Camera.main.GetComponent<CameraMovement>();
		onDoorTrigger = false;
		direction = Direction.Down;

		coroutine_FlipX = false;
		coroutine_SideWalk = false;
	}

	void FixedUpdate()
	{
		if(cam.Panning) return;
		float old_x = transform.position.x;
		float old_y = transform.position.y;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			sr.sprite = back;
			if(!coroutine_FlipX) StartCoroutine("FlipX");
			direction = Direction.Up;
			transform.position += new Vector3(0f, moveSpeed, 0f);
		}
		else if(Input.GetKey(KeyCode.DownArrow))
		{
			sr.sprite = front;
			if(!coroutine_FlipX) StartCoroutine("FlipX");
			direction = Direction.Down;
			transform.position += new Vector3(0f, -moveSpeed, 0f);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			sr.flipX = false;
			if(!coroutine_SideWalk) StartCoroutine("SideWalk");
			direction = Direction.Left;
			transform.position += new Vector3(-moveSpeed, 0f, 0f);
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			sr.flipX = true;
			if(!coroutine_SideWalk) StartCoroutine("SideWalk");
			direction = Direction.Right;
			transform.position += new Vector3(moveSpeed, 0f, 0f);
		}
		else StopFlipping();
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

	public void StopFlipping()
	{
		StopCoroutine("FlipX");
		coroutine_FlipX = false;
		StopCoroutine("SideWalk");
		coroutine_SideWalk = false;
	}

	IEnumerator FlipX()
	{
		coroutine_FlipX = true;
		while(true)
		{
			sr.flipX = !sr.flipX;
			yield return new WaitForSeconds(0.25f);
		}
	}

	IEnumerator SideWalk() //possibly non-extensible
	{
		coroutine_SideWalk = true;
		while(true)
		{
			foreach(Sprite s in walkAnimation)
			{
				sr.sprite = s;
				yield return new WaitForSeconds(0.25f);
			}
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
