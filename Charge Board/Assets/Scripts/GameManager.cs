using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool freezeTime;
    public static bool lockControls;
    
    // Start is called before the first frame update
    void Start()
    {
        freezeTime = false;
        lockControls = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
