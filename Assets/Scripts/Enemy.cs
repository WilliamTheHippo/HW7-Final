using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float SwordHit; //General Hit Function

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hi, I'm an Enemy!");

        SwordHit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
