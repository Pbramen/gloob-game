using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public gloob gloob;
    public Rigidbody2D rd;

    //Handles Player Input
    void Update(){
        float maxGravity;
        float velocityY = rd.velocity.y;
        bool ground = gloob.isGround();
        
        gloob.Horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump") && ground){
            gloob.Jump = true;
            Debug.Log("Jump Running");
        }
        else if(!ground && (Input.GetButtonUp("Jump") ||velocityY < 0.0f)){
            gloob.JumpApex = true;
        }
        if(Input.GetButtonUp("Horizontal")){
            gloob.stop();
        }
        if(velocityY < (maxGravity = gloob.MaxGravity)){
            gloob.capGravity();
        }
    }

  
}