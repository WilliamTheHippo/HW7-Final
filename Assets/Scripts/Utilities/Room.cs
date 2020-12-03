using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public Vector2Int coords;
	public Room up, down, left, right;
	public bool selfSealing;
	public List<Door> doors;
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
	}

	IEnumerator Doors()
	{
		foreach(Door door in doors) StartCoroutine(door.Close());
		yield return new WaitUntil(() => {
			bool flag = true;
			foreach(Enemy e in enemies) if(e != null) flag = false;
			return flag;
		});
		foreach(Door door in doors) StartCoroutine(door.Open());
	}
}
