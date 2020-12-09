using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
	Chest chest;

	void Start()
	{
		chest = GetComponentInParent<Chest>();
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if(Input.GetKeyDown(KeyCode.Z))
		{
			chest.Open();
			GetComponent<Collider2D>().enabled = false;
		}
	}
}
