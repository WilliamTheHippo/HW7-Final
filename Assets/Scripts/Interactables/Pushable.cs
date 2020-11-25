using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{

    Player player; 

    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // This class will move the object up by one tile once the player has been 
    // pushing on it for long enough.

    IEnumerator Move() {
        yield return null;
    }
    
}