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

	Room room;

	Collider2D c;
	Renderer r;

	public virtual void Start()
	{
		c = GetComponent<Collider2D>();
		r = GetComponent<Renderer>();
	}

	public IEnumerator Appear()
	{
		if(visible) yield break;
		if(condition == Condition.AllEnemiesDead)
			yield return new WaitUntil(room.AllEnemiesDead);
		visible = true;
		c.enabled = true;
		r.enabled = true;
	}

	public IEnumerator Disappear()
	{
		if(!visible) yield break;
		if(condition == Condition.AllEnemiesDead)
			yield return new WaitUntil(room.AllEnemiesDead);
		visible = false;
		c.enabled = false;
		r.enabled = false;
	}
}
