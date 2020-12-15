using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Switch : Interactable
{
	public Room room;
	public bool active;

	Renderer r;

	void Start()
	{
		active = false;
		r = GetComponent<Renderer>();
		r.enabled = true;
	}

	public override void Activate()
	{
		if(!active)
		{
			active = true;
			r.enabled = false;
			foreach(Conditional c in room.conditionals)
			{
				if(c.appearCondition == Conditional.Condition.Switch)
					c.Appear();
			}
		}
	}
}
