using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gloob: MonoBehaviour{

    [SerializeField]  Rigidbody2D rd;
    [SerializeField] SpriteRenderer spriteR;
    [SerializeField] BoxCollider2D box2D;
    [SerializeField] LayerMask platformMask;
    [SerializeField] AnimtionScript anm;

    [Header ("Movement")]
    [SerializeField] float speed = 2.5f;
    [SerializeField] bool jump = false, jumpApex = false;
    [SerializeField] int maxNumJump = 1, curJumps = 0;
    [SerializeField] float jumpHeight = 5f, fallSpeed = 200f;
    [SerializeField] float maxGravity;
    [SerializeField] float horizontal;
    [SerializeField] float aerialMovement = 2f;
   [Header ("Events")]
    public UnityEvent gemPickUp;
   
    void Start(){
        rd = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    // private void FixedUpdate() {
    //     Move();
    //     gloobJump();   
    //     resetJump();
    // }
    //Moves Gloob and changes animation
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
    // Adds jumping mechanic using ground check
    public void gloobJump(){
        if(jump){
            curJumps++;
            jump=false;
            float jForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rd.gravityScale));
            //prevent multiple jumps from exponentially increasing jump
            rd.velocity = Vector2.zero;
            rd.AddForce(new Vector2(rd.velocity.x * aerialMovement, jForce), ForceMode2D.Impulse);
            if(Input.GetAxisRaw("Horizontal") == 0f){
                rd.velocity = new Vector2(0, rd.velocity.y);
            }
        }
        if(jumpApex){
            jumpApex = false;
            rd.AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);
        }
    }
    //==================== Helper functions ==================== 
    // stops gloob's vertical movement.
    public void canJump(){
        if(curJumps < maxNumJump){
            jump=true;
        }
    }
    public void resetJump(){
        if(isGround() && (rd.velocity.y > -.05f && rd.velocity.y < .05f)){
            curJumps = 0;
        }
    }
    public void stop(){
         rd.velocity = (new Vector2 (0, rd.velocity.y));
    }
    // enforces max fall speed on gloob.
    public void capGravity(){
        rd.velocity=(new Vector2(rd.velocity.x, maxGravity));
    }
    // ground check
    public bool isGround(){
        return Physics2D.BoxCast(box2D.bounds.center, box2D.bounds.size, 0f, Vector2.down, 0.1f, platformMask);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("gem")){
            test();
        }
    }
    public void test(){
        gemPickUp?.Invoke();
    }
    // ==================== Properties ====================
    public Rigidbody2D RD{
        get{
            return rd;
        }
        set{
            rd = value;
        }
    }
    public bool Jump{
        get{
            return jump;
        }
        set{
            jump = value;
        }
    }
    public bool JumpApex{
        get{
            return jumpApex;
        }
        set{
            jumpApex = value;
        }
    }
    public float MaxGravity{
        get{
            return maxGravity;
        }
        set{
            maxGravity = value;
        }
    }

    public float Horizontal{
        get{
            return horizontal;
        }
        set{
            horizontal = value;
        }
    }

    public int CurJumps{
        get{
            return curJumps;
        }
        set{
            curJumps = value;
        }
    }
    public int MaxNumJump{
        get{
            return maxNumJump;
        }
        set{
            maxNumJump = value;
        }
    }
}

