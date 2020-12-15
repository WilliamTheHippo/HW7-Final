using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakFloor : Interactable
{
	public bool broken;
	Tilemap hole;
	Renderer r;
	Collider2D c;

	void Start()
	{
		broken = false;
		hole = transform.GetChild(0).GetComponent<Tilemap>();
		hole.GetComponent<Collider2D>().enabled = false;
		r = GetComponent<Renderer>();
		c = GetComponent<Collider2D>();
		r.enabled = true;
		c.enabled = true;
	}

	void Activate() {StartCoroutine(Break());}

	IEnumerator Break()
	{
		Debug.Log("floor breaks");
		c.enabled = false;
		yield return new WaitForSeconds(0.5f);
		r.enabled = false;
		hole.GetComponent<Collider2D>().enabled = true;
	}
}
