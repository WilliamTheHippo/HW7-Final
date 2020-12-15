using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WeakFloor : Interactable
{
	public bool broken;
	Collider2D hole;
	Renderer r;
	Collider2D c;

	public AudioClip breakSound;
	AudioSource sound;

	void Start()
	{
		broken = false;
		hole = transform.GetChild(0).GetComponent<Collider2D>();
		hole.enabled = false;
		r = GetComponent<Renderer>();
		c = GetComponent<Collider2D>();
		sound = GetComponent<AudioSource>();
		r.enabled = true;
		c.enabled = true;
	}

	public override void Activate() {StartCoroutine(Break());}

	IEnumerator Break()
	{
		sound.clip = breakSound;
		sound.Play();
		c.enabled = false;
		yield return new WaitForSeconds(0.75f);
		r.enabled = false;
		hole.enabled = true;
	}
}
