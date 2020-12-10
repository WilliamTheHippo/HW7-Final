﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chest : Conditional
{
	public Tile openTile;

	Tilemap tilemap;

	public override void Start()
	{
		base.Start();
		tilemap = GetComponent<Tilemap>();
	}

	public void Open()
	{
		tilemap.SetTile(tilemap.origin + new Vector3Int(0,1,0), openTile);
		tilemap.SetTile(tilemap.origin + new Vector3Int(1,1,0), openTile);
	}
}
