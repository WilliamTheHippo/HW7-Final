using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Input_tmp : MonoBehaviour
{
	public Tilemap tileMap;
	public Sprite sprite;

	void Update()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			Camera.main.transform.position += new Vector3(0,1,0);
		}
	}
}
