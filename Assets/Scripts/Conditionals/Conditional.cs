using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditional : MonoBehaviour
{
	public bool visible;
	public bool alsoDisappears;
	public enum Condition {
		AllEnemiesDead,
		Immediately
	}
	public Condition condition;
	public Room room;

	protected AudioSource sound;
	public AudioClip appearSound, disappearSound;

	Collider2D c;
	Renderer r;

	public virtual void Start()
	{
		sound = GetComponent<AudioSource>();
		c = GetComponent<Collider2D>();
		r = GetComponent<Renderer>();
	}

	public IEnumerator Appear()
	{
		if(visible) yield break;
		if(condition == Condition.AllEnemiesDead)
			yield return new WaitUntil(room.AllEnemiesDead);
		sound.clip = appearSound;
		sound.Play();
		visible = true;
		c.enabled = true;
		r.enabled = true;
	}

	public IEnumerator Disappear()
	{
		if(!visible) yield break;
		if(condition == Condition.AllEnemiesDead)
			yield return new WaitUntil(room.AllEnemiesDead);
		if(disappearSound != null)
		{
			sound.clip = disappearSound;
			sound.Play();
		}
		visible = false;
		c.enabled = false;
		r.enabled = false;
	}
}
