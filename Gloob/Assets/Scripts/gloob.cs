using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float timer = 0f, Friction=0.5f;
    [SerializeField] float horizontal;
   

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
    }
    
    public void FixedUpdate(){
        Move();
        gloobJump();
    }


    public void Move(){

        if(horizontal !=0){
            rd.velocity = new Vector2(horizontal * speed, ((rd.velocity.y)));
            anm.ChangeAnimationState("Walking", 1);
            timer =0;
            if(horizontal < 0){
                spriteR.flipX = true;
            }
            else{
                spriteR.flipX = false;
            }
            
        }
        else if(rd.velocity.x != 0f)
        {
            timer += Time.fixedDeltaTime;
            float newSpeed = Mathf.Lerp(speed, 0f, timer / Friction);
            if (rd.velocity.x < 0)
                newSpeed = -newSpeed;
            rd.velocity = new Vector2(newSpeed, 0f);
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
        else if(jumpApex){
            jumpApex = false;
            rd.AddForce(Vector2.down * fallSpeed);
        }
        
    }
    private bool isGround(){
        return Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, 0.1f, platformMask);
    }
}