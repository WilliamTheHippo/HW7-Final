using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class fall : MonoBehaviour
{
    float ResetTimer = 0.8f;
    
    void Update()
    {   
        ResetTimer -= Time.deltaTime;
        Debug.Log(ResetTimer);
        if(ResetTimer <= 0f){
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

             Debug.Log("Scene Reset");
        }
    }
}
