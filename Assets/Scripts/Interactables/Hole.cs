using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
    	if(c.tag == "Enemy") c.GetComponent<Enemy>().Fall();
    	if(c.tag == "Player") c.GetComponent<Player>().Fall();
    }
}
