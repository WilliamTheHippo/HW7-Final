﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Enemy
{
	void Start()
	{
		crystal = true;
		SetupEnemy();
		noSwordHit = true;
		canKnockback = false;
		DeathScale = new Vector3(1f,1f,1f);
	}
}
