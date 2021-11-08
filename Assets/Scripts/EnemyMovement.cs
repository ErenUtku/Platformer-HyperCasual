using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movespeed = 1f;


    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBody;
    Transform enemyPos;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (facingRight())
        {
            myRigidbody.velocity = new Vector2(movespeed, 0);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-movespeed, 0);
        }
    }
    bool facingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

}
