using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
	public bool open;

	Tilemap tilemap;
	TileBase[] reserve;
	Renderer r;
	Collider2D c;

	void Start()
	{
		tilemap = GetComponent<Tilemap>();
		r = GetComponent<Renderer>();
		c = GetComponent<Collider2D>();
		reserve = new TileBase[4];
		reserve[0] = tilemap.GetTile(tilemap.origin);
		reserve[1] = tilemap.GetTile(tilemap.origin + new Vector3Int(0,1,0));
		reserve[2] = tilemap.GetTile(tilemap.origin + new Vector3Int(1,0,0));
		reserve[3] = tilemap.GetTile(tilemap.origin + new Vector3Int(1,1,0));
		if(open)
		{
			HalfOpen();
			r.enabled = false;
			c.enabled = false;
		}
	}

	void HalfOpen()
	{
		tilemap.SetTile(
			tilemap.origin + new Vector3Int(0,1,0),
			tilemap.GetTile(tilemap.origin)
		);
		tilemap.SetTile(tilemap.origin, null);
		tilemap.SetTile(
			tilemap.origin + new Vector3Int(1,1,0),
			tilemap.GetTile(tilemap.origin + new Vector3Int(1,0,0))
		);
		tilemap.SetTile(tilemap.origin + new Vector3Int(1,0,0), null);
	}

	public IEnumerator Open()
	{
		HalfOpen();
		yield return new WaitForSeconds(.25f);
		r.enabled = false;
		c.enabled = false;
		open = true;
	}

	public IEnumerator Close()
	{
		r.enabled = true;
		c.enabled = true;
		yield return new WaitForSeconds(.25f);
		// tilemap.SetTile(
		// 	tilemap.origin,
		// 	tilemap.GetTile(tilemap.origin + new Vector3Int(0,1,0))
		// );
		tilemap.SetTile(tilemap.origin, reserve[0]);
		tilemap.SetTile(tilemap.origin + new Vector3Int(0,1,0), reserve[1]);
		// tilemap.SetTile(
		// 	tilemap.origin + new Vector3Int(1,0,0),
		// 	tilemap.GetTile(tilemap.origin + new Vector3Int(1,1,0))
		// );
		tilemap.SetTile(tilemap.origin + new Vector3Int(1,0,0), reserve[2]);
		tilemap.SetTile(tilemap.origin + new Vector3Int(1,1,0), reserve[3]);
		open = false;
	}
}
