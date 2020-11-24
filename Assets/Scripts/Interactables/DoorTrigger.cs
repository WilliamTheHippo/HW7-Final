using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
	public Door door;
	CameraMovement cam;

	void Start()
	{
		cam = Camera.main.GetComponent<CameraMovement>();
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if(c.tag == "Player")
		{
			if(door.open) StartCoroutine(CloseDoor());
			GetComponent<Collider2D>().enabled = false;
		}
	}

	public IEnumerator CloseDoor()
	{
		yield return new WaitUntil(() => cam.Panning == true);
		yield return new WaitUntil(() => cam.Panning == false);
		StartCoroutine(door.Close());
	}
}
