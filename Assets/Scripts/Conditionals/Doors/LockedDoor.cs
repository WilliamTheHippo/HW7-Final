using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door
{
	public bool locked;

	void Start()
	{
		locked = true;
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			Player p = c.GetComponent<Player>();
			if(p.keys == 0) return;
			else
			{
				p.keys--;
				locked = false;
				StartCoroutine(Open());
			}
		}
	}

	public override IEnumerator Open()
	{
		if(locked) yield break;
		else base.Open();
	}
}
