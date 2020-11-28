using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chest : MonoBehaviour
{
	public Tile openTile;

	Tilemap tilemap;

	void Start()
	{
		tilemap = GetComponent<Tilemap>();
	}

	public void Open()
	{
		tilemap.SetTile(tilemap.origin + new Vector3Int(0,1,0), openTile);
		tilemap.SetTile(tilemap.origin + new Vector3Int(1,1,0), openTile);
	}
}
