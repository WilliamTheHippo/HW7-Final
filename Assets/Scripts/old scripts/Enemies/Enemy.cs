using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    AttackAndMove player_t;
	Vector2Int room;
    public abstract void SwordHit();
    public abstract void OnUpdate();

    public void Start()
    {
        player_t = GameObject.Find("Player").GetComponent<AttackAndMove>();
        if(transform.parent.GetComponent<Room>() != null)
        {
            room.x = transform.parent.GetComponent<Room>().coords.x;
            room.y = transform.parent.GetComponent<Room>().coords.y;
        }
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
