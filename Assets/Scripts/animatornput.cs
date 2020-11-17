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

    }
}
