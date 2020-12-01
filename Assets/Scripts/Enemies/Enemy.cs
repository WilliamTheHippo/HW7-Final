using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    AttackAndMove player_t;
	public int room; //very simple hack, change later
    public abstract void SwordHit();
    public abstract void OnUpdate();

    public void Start()
    {
        player_t = GameObject.Find("Player").GetComponent<AttackAndMove>();
        //Debug.Log(player_t.name);
    }

    void FixedUpdate()
    {
        if(room != player_t.room) return;
        OnUpdate();
    }

    public void Fall()
    {
        //Debug.LogError("Haven't implemented falling yet!");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Fall") Fall();
    }
}
