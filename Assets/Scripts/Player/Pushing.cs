using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour
{
    public float moveSpeed;

    [HideInInspector]
    public Pushable target; 
    Player player; 
    Walking walk;
    void Start()
    {
        player = GetComponent<Player>();
        walk = GetComponent<Walking>();
    }

    public void Push() { 
        if (target != null) {
            
        }
    }

    public void Stop() {
        StopCoroutine("Animation");
        target = null;
    } 

    IEnumerator Animation() {
        yield return null;
    }

}