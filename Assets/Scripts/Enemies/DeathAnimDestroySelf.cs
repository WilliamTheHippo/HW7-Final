using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimDestroySelf : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.8f);
    }
}