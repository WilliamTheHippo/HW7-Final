using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatornput : MonoBehaviour
{
    Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>(); // find animator thats on the game Object      
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)){
            myAnimator.SetInteger("walkingstate", 2);
        } else if (Input.GetKey(KeyCode.DownArrow)){
             myAnimator.SetInteger("walkingstate", 3);
        }else if (Input.GetKey(KeyCode.LeftArrow)){
             myAnimator.SetInteger("walkingstate", 4);
        } else if (Input.GetKey(KeyCode.UpArrow)){
            myAnimator.SetInteger("walkingstate", 1);
        }
    }
}
