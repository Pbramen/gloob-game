using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    bool pausing = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && !pausing){
            Debug.Log("Paused");
            pausing=true;
            pause();
        }
        else if (Input.GetButtonDown("Cancel") && pausing){
            Debug.Log("Resuming");
            pausing=false;
            resume();
        } 
    }
    void pause(){
        Time.timeScale = 0;
    }
    void resume(){
        Time.timeScale = 1;
    }
}
