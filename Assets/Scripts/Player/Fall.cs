using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : PlayerState 
{
	public override void UpdateOnActive()
	{
		if(firstFrame)
		{
			//disable animators
			//set player.sr.sprite to falling
			//call player.Die(), which'll just reload the scene
		}
	}
}
