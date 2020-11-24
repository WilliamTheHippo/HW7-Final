using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shielding : MonoBehaviour
{
	public float moveSpeed;
	public float knockback;

	[Header("Sprites")]
	public Sprite[] sprites;
	public Sprite front, back, side;

	CameraMovement cam;
	SpriteRenderer sr;
	Player player;
	Walking walk;

	int offset;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		cam = Camera.main.GetComponent<CameraMovement>();
		player = GetComponent<Player>();
		walk = GetComponent<Walking>();
	}

	public void Shield()
	{
		if(!player.shielding)
		{
			if(Input.GetKey(KeyCode.UpArrow)) player.direction = Player.Direction.Up;
			else if(Input.GetKey(KeyCode.DownArrow)) player.direction = Player.Direction.Down;
			else if(Input.GetKey(KeyCode.LeftArrow)) player.direction = Player.Direction.Left;
			else if(Input.GetKey(KeyCode.RightArrow)) player.direction = Player.Direction.Right;
			else
			{
				offset = 0;
				if(player.direction == Player.Direction.Up) sr.sprite = back;
				else if(player.direction == Player.Direction.Down)
				{
					offset += 2;
					sr.sprite = front;
				}
				else
				{
					offset += 4;
					sr.sprite = side;
				}
			}
			StartCoroutine("Animation");
			player.shielding = true;
		}
		if(Input.GetKey(KeyCode.UpArrow)) transform.position += new Vector3(0f, moveSpeed, 0f);
		if(Input.GetKey(KeyCode.DownArrow)) transform.position += new Vector3(0f, -moveSpeed, 0f);
		if(Input.GetKey(KeyCode.LeftArrow)) transform.position += new Vector3(-moveSpeed, 0f, 0f);
		if(Input.GetKey(KeyCode.RightArrow)) transform.position += new Vector3(moveSpeed, 0f, 0f);
	}

	public void Stop() {StopCoroutine("Animation");}

	IEnumerator Animation()
	{
		while(true)
		{
			sr.sprite = sprites[offset];
			yield return new WaitForSeconds(0.25f);
			sr.sprite = sprites[offset+1];
			yield return new WaitForSeconds(0.25f);
		}
	}
}
