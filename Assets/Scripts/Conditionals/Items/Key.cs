using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
	public enum KeyType{Regular, Boss}
	public KeyType type;
	public Sprite regular, boss;

	SpriteRenderer sr;

	void Start()
	{
		base.Start();
		sr = GetComponent<SpriteRenderer>();
		if(type == KeyType.Boss) sr.sprite = boss;
		else sr.sprite = regular;
	}

	public override void OnPickup()
	{
		base.OnPickup();
		player.keys++;
	}
}
