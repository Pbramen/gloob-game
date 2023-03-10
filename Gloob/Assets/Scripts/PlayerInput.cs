using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public gloob gloob;

    public void FixedUpdate(){
        gloob.Move();
        gloob.gloobJump();
    }
  
}