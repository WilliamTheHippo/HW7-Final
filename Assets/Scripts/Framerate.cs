using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framerate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       QualitySettings.vSyncCount = 0;  // VSync must be disabled
        // Make the game run as fast at 60 fps
        Application.targetFrameRate = 60;
    }
}
