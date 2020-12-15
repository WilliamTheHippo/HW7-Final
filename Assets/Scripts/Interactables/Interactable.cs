using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public bool enemiesCanTrigger;

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Enemy" && enemiesCanTrigger) ActivateEnemy();
		if(c.tag == "Player") Activate();
	}

	public virtual void Activate()
	{
		Debug.LogError(gameObject.name + " has no Activate() function!");
	}

	public virtual void ActivateEnemy()
	{
		Debug.LogError(gameObject.name + " is marked as trigger-able by an enemy, but has no ActivateEnemy()!");
	}
}
