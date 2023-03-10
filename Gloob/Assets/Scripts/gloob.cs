using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gloob: MonoBehaviour{

    [SerializeField] Rigidbody2D rd;
    [SerializeField] SpriteRenderer spriteR;
    [SerializeField] BoxCollider2D box2D;
    [SerializeField] LayerMask platformMask;
    [SerializeField] AnimtionScript anm;

    [Header ("Movement")]
    [SerializeField] float speed = 2.5f;
    public bool jump = false, jumpApex = false;
    [SerializeField] float jumpHeight = 5f, fallSpeed = 200f;
    [SerializeField] float Friction=0.25f, maxGravity;
    [SerializeField] float horizontal;
   
   [Header ("Events")]
    public UnityEvent gemPickUp;
   

    void Start(){
        rd = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }


    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");
        bool ground = isGround();
        if(Input.GetButtonDown("Jump") && ground){
            
            jump = true;
        }
        else if(!ground && (Input.GetButtonUp("Jump") || rd.velocity.y < 0.0f)){
            jumpApex = true;
        }
        //implements very simplistic friction
        if(Input.GetButtonUp("Horizontal")){
            rd.velocity = (new Vector2 (rd.velocity.x * Friction, rd.velocity.y));
        }
        if(rd.velocity.y < maxGravity){
            rd.velocity=(new Vector2(rd.velocity.x, maxGravity));
        }
    }
    

    public void Move(){

        if(horizontal !=0){
            rd.velocity = new Vector2(horizontal * speed, ((rd.velocity.y)));
            anm.ChangeAnimationState("Walking", 1);
            if(horizontal < 0){
                spriteR.flipX = true;
            }
            else{
                spriteR.flipX = false;
            }
            
        }
        else if (rd.velocity == Vector2.zero){           
            anm.ChangeAnimationState("Idle", 1);
        }
        
    }
    public void gloobJump(){
        if(jump){
            jump=false;
            float jForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rd.gravityScale));
            rd.AddForce(new Vector2(rd.velocity.x * 1.5f, jForce), ForceMode2D.Impulse);
            if(Input.GetAxisRaw("Horizontal") == 0f){
                rd.velocity = new Vector2(0, rd.velocity.y);
            }
        }
        if(jumpApex){
            jumpApex = false;
            rd.AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);
        }
    }

    private bool isGround(){
        return Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, 0.1f, platformMask);
    }
        void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("platform")){
            rd.velocity = (new Vector2(rd.velocity.x, 0));
        }
    }
}