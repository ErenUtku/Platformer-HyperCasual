using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    
    public float runspeed = 5f;
    public float jumpspeed = 5f;
    public float climbspeed = 5f;
    

    //State

    bool isAlive = true;
    
    //Cached components
    
    Rigidbody2D myrigidbody;
    Transform playerpos;
    Animator myanimator;
    CapsuleCollider2D BodyCollider;
    BoxCollider2D MyFeet;
    float gravityScaleAtStart;
    public int Points = 100;





    void Start()
    {
       
        myrigidbody = GetComponent<Rigidbody2D>();
        playerpos = GetComponent<Transform>();
        myanimator = GetComponent <Animator>();
        BodyCollider = GetComponent<CapsuleCollider2D>();
        MyFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myrigidbody.gravityScale;

    }

    void Update()
    {
        Die();
        if (!isAlive) { return; }
        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        



    }
   private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runspeed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        
        bool IsRunning = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        myanimator.SetBool("RunActive", IsRunning);
        
        
    }


    private void ClimbLadder()
    {

        if (!MyFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myanimator.SetBool("ClimbActive", false);
            myrigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
       

            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //-1 to 1
            Vector2 climbVelocity = new Vector2(myrigidbody.velocity.x, controlThrow * climbspeed);
            myrigidbody.velocity = climbVelocity;
            myrigidbody.gravityScale = 0f;
       
            bool IsClimbing = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;
            myanimator.SetBool("ClimbActive", IsClimbing);
        
        
    }

    
    private void Jump()
    {
        if (!MyFeet .IsTouchingLayers(LayerMask.GetMask("Foreground"))) { return; }

            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpspeed);
                myrigidbody.velocity += jumpVelocity;
            }
        
    }
  
    private void FlipSprite()
    {
        bool playerMovementSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
        if (playerMovementSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }
    private void Die()
    {
        if (myrigidbody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            StartCoroutine(DelayDeath());                    
        }
       
    }
    IEnumerator DelayDeath()
    {
        isAlive = false;
        myanimator.SetTrigger("Dying");
        myrigidbody.velocity = new Vector2(25f, 25f);
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
 

}
