using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtionScript : MonoBehaviour

{
    public Animator animator;
    public string currentState ="Idle";

    public void ChangeAnimationState(string newState, float speed=1){
        animator.speed=speed;
        if(currentState == newState){
            return;
        }
        currentState=newState;
        animator.Play(newState);
    }

}
