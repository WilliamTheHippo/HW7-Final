using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
	public float moveSpeed;
	public Sprite[] sprites;

	int offset;

	CameraMovement cam;
	SpriteRenderer sr;
	Player player;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		cam = Camera.main.GetComponent<CameraMovement>();
		player = GetComponent<Player>();
	}

	public void Walk()
	{
		float old_x = transform.position.x;
		float old_y = transform.position.y;
		offset = 0;
		if(player.direction == Player.Direction.Down) offset += 2;
		if(player.direction == Player.Direction.Left || player.direction == Player.Direction.Right) offset += 4;
		sr.flipX = player.direction == Player.Direction.Left ? true : false;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			player.direction = Player.Direction.Up;
			if(!player.walking) StartCoroutine("Animation");
			player.walking = true;
			transform.position += new Vector3(0f, moveSpeed, 0f);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			player.direction = Player.Direction.Down;
			if(!player.walking) StartCoroutine("Animation");
			player.walking = true;
			transform.position += new Vector3(0f, -moveSpeed, 0f);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			player.direction = Player.Direction.Left;
			if(!player.walking) StartCoroutine("Animation");
			player.walking = true;
			transform.position += new Vector3(-moveSpeed, 0f, 0f);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			player.direction = Player.Direction.Right;
			if(!player.walking) StartCoroutine("Animation");
			player.walking = true;
			transform.position += new Vector3(moveSpeed, 0f, 0f);
		}
		QuantizePosition();
		SwitchRoom(old_x, old_y);
	}

	public void Stop() {StopCoroutine("Animation");}

	public void QuantizePosition()
	{
		float x = Mathf.Round(transform.position.x * 8) / 8;
		float y = Mathf.Round(transform.position.y * 8) / 8;
		transform.position = new Vector3(x,y,0f);
	}

	public void SwitchRoom(float old_x, float old_y)
	{
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

	IEnumerator Animation() //can't just use FlipX anymore because shield sprites aren't mirrored
	{
		while(true)
		{
			sr.sprite = sprites[offset];
			yield return new WaitForSeconds(0.125f);
			sr.sprite = sprites[offset]; //duplicates improve game feel
			yield return new WaitForSeconds(0.125f);
			sr.sprite = sprites[offset+1];
			yield return new WaitForSeconds(0.125f);
			sr.sprite = sprites[offset+1];
			yield return new WaitForSeconds(0.125f);
		}
	}
}
