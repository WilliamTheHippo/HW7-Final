using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : PlayerState 
{
	public override void UpdateOnActive()
	{
		if(firstFrame)
		{
			linkAnimator.enabled = false;
			//new anim
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
