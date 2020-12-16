using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chest : Conditional
{
	public bool open;
	public Item itemPrefab;
	public Tile openTile;

	Tilemap tilemap;

	public AudioClip itemSound;

	public override void Start()
	{
		base.Start();
		open = false;
		sound = GetComponent<AudioSource>();
		tilemap = GetComponent<Tilemap>();
	}

	public void Open()
	{
		if(open) return;
		open = true;
		sound.clip = itemSound;
		Item item = Instantiate(
			itemPrefab,
			new Vector3(
				transform.position.x,
				transform.position.y - 3f, 
				0
			),
		Quaternion.identity);
		room.conditionals.Add(item);
		StartCoroutine(item.Appear());
		StartCoroutine(item.Disappear());
		sound.Play();
		tilemap.SetTile(tilemap.origin + new Vector3Int(0,1,0), openTile);
		tilemap.SetTile(tilemap.origin + new Vector3Int(1,1,0), openTile);
	}
}
