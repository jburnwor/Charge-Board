using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static bool freezeTime;
    public static bool lockControls;
    public static bool stopEnemies;
    public static float enemiesLeft = 3;
    public Text winText;
    float time;
    float timerTime = 20f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        freezeTime = false;
        lockControls = false;

        winText.enabled = false;
        time = 0;
        stopEnemies = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesLeft <= 0)
        {
            winText.enabled = true;
            time += Time.deltaTime;
            //Debug.Log(time);
            if (time >= timerTime)
            {
                time = 0;
                SceneManager.LoadScene("Menu 3D");
            }
        }
        
        
    }
}
