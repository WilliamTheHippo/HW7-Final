using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
	public Sprite[] sprites;
	public Sprite[] swordSprites;
	public GameObject sword;

	Vector3[] swordPositions;

	SpriteRenderer sr;
	SpriteRenderer sword_sr;
	Transform swordScaler;
	Player player;

	int offset;
	bool rotate;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		player = GetComponent<Player>();
		sword_sr = sword.GetComponent<SpriteRenderer>();
		swordScaler = sword.transform.parent;
		swordPositions = new Vector3[3];
		rotate = false;
	}

	public void Attack()
	{
		offset = 4; //subtraction instead of addition, just to keep things fun
		if(player.direction == Player.Direction.Down) offset -= 2;
		if(player.direction == Player.Direction.Up) offset -= 4;
		if(!player.attacking) StartCoroutine("SwingSword");
		player.attacking = true;
		if(player.direction == Player.Direction.Up || player.direction == Player.Direction.Down)
		{
			swordPositions[0] = new Vector3(2f,0f,0f);
			swordPositions[1] = new Vector3(1.5f,1.5f,0f);
			swordPositions[2] = new Vector3(0f,2f,0f);
		}
		if(player.direction == Player.Direction.Left || player.direction == Player.Direction.Right)
		{
			swordPositions[0] = new Vector3(0f,2f,0f);
			swordPositions[1] = new Vector3(1.5f,1.5f,1.5f);
			swordPositions[2] = new Vector3(2f,0f,0f);
		}
		if(player.direction == Player.Direction.Down) swordScaler.localScale = new Vector3(1f,-1f,0f);
		else if(player.direction == Player.Direction.Left) swordScaler.localScale = new Vector3(-1f,1f,0f);
		else swordScaler.localScale = new Vector3(1f,1f,0f);
	}

	public void Stop()
	{
		StopCoroutine("SwingSword");
		sword.SetActive(false);
	}

	IEnumerator SwingSword()
	{
		sword_sr.flipX = false;
		if(rotate) sword.transform.Rotate(0,0,90);
		rotate = false;
		sword.transform.localPosition = swordPositions[0];
		sr.sprite = sprites[offset];
		sword_sr.sprite = swordSprites[0];
		sword.SetActive(true);
		yield return new WaitForSeconds(0.125f);
		sr.sprite = sprites[offset+1];
		sword_sr.sprite = swordSprites[1];
		sword.transform.localPosition = swordPositions[1];
		yield return new WaitForSeconds(0.125f);
		sword.transform.localPosition = swordPositions[2];
		if(player.direction == Player.Direction.Left || player.direction == Player.Direction.Right)
			rotate = true;
		if(rotate)
		{
			sword.transform.Rotate(0,0,-90);
			sword_sr.flipX = true;
		}
		sword_sr.sprite = swordSprites[0];
	}
}
