using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gloob: MonoBehaviour{

    [SerializeField] Rigidbody2D rd;
    public SpriteRenderer spriteR;
    [SerializeField] AnimtionScript anm;
    [Header ("Movement")]
    [SerializeField] float speed = 2.5f;
    [SerializeField] bool isJumping = false;
    public bool jump = false, jumpApex = false;
    public float jumpHeight = 5f, fallSpeed = 200f;
    public float timer = 0f;
    public float horizontal , damperTime=0.5f;
   

    void Start(){
        rd = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }


    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump") && !isJumping){
            jump = true;
        }
        else if(isJumping && (Input.GetButtonUp("Jump") || rd.velocity.y < 0.0f)){
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
 
            float newSpeed = Mathf.Lerp(speed, 0f, timer / damperTime);
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
            jumpApex=false;
            rd.AddForce(Vector2.down * fallSpeed);
        }
        
    }
    private bool isGround(){
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
       if(other.gameObject.CompareTag("platform")){
          
            isJumping=false;
        }
        else{
            Debug.Log("Unexpected Collision " + other.gameObject.name);
        }
        
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("platform")){
            isJumping=true;
        }
    }
}
