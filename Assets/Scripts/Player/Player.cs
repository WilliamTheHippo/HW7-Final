using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("Sprites")]
	public Sprite front;
	public Sprite back;
	public Sprite side;
	Sprite idle;

	public enum Direction {Up, Down, Left, Right}

	[Header("State Management")]
	public Direction direction;
	public bool walking;
	public bool shielding;
	public bool attacking;

	SpriteRenderer sr;
	CameraMovement cam;
	Walking walk;
	Shielding shield;
	Attacking attack;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		cam = Camera.main.GetComponent<CameraMovement>();
		idle = sr.sprite;
		direction = Direction.Down;

		walk = GetComponent<Walking>();
		walking = false;
		shield = GetComponent<Shielding>();
		shielding = false;
		attack = GetComponent<Attacking>();
	}

	void FixedUpdate()
	{
		if(cam.Panning) return;
		if(Input.GetKey(KeyCode.Z)) {attack.Attack(); return;}
		if(Input.GetKey(KeyCode.X)) {shield.Shield(); return; }
		if( Input.GetKey(KeyCode.UpArrow) ||
			Input.GetKey(KeyCode.DownArrow) ||
			Input.GetKey(KeyCode.LeftArrow) ||
			Input.GetKey(KeyCode.RightArrow)
			) { walk.Walk(); return; }
		Idle();
	}

	public void Idle()
	{
		walking = false;
		walk.Stop();
		shielding = false;
		shield.Stop();
		attacking = false;
		attack.Stop();

		if(direction == Direction.Up) sr.sprite = back;
		else if(direction == Direction.Down) sr.sprite = front;
		else sr.sprite = side;
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
