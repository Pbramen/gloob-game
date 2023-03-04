using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gloob: MonoBehaviour{

    Rigidbody2D rd;
    SpriteRenderer spriteR;
    public AnimtionScript anm;
    public float speed = 2.5f;
    void Start(){
        rd = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }
    public void Move(Vector3 offset){
        if(offset !=Vector3.zero){
            Debug.Log(offset);
            rd.MovePosition(transform.position + ((offset) * speed));
            anm.ChangeAnimationState("Walking");

            if(offset.x < 0){
                spriteR.flipX = true;
            }
            else{
                spriteR.flipX = false;
            }
        }
        else{
            anm.ChangeAnimationState("Idle");
        }
    }
}
