using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public Vector2Int coords;
	public Room up, down, left, right;
	public bool selfSealing;
	public List<Door> doors;
	public List<Conditional> conditionals;
	public Switch t_switch;
	List<Enemy> enemies;

	bool unvisited;

	void Start()
	{
		unvisited = true;
		enemies = new List<Enemy>();
		foreach(Transform child in transform)
			enemies.Add(child.GetComponent<Enemy>());
	}

	public void Initialize()
	{
		if(!unvisited) return;
		unvisited = false;
		if(selfSealing) StartCoroutine("Doors");
		foreach(Conditional c in conditionals)
		{
			c.room = this;
			StartCoroutine(c.Appear());
			if(c.disappearCondition != Conditional.Condition.Never)
				StartCoroutine(c.Disappear());
		}
	}

	public void Reset()
	{
		unvisited = true;
		//respawn enemies? (probably just reload scene)
	}

	IEnumerator Doors() //open on all enemies killed is hardcoded in for now
	{
		foreach(Door door in doors) StartCoroutine(door.Close());
		yield return new WaitUntil(() => {
			bool flag = true;
			foreach(Enemy e in enemies) if(e != null) flag = false;
			return flag;
		});
		foreach(Door door in doors) StartCoroutine(door.Open());
	}

	public bool AllEnemiesDead()
	{
		bool flag = true;
		foreach(Enemy e in enemies) if(e != null) flag = false;
		return flag;
	}
}
