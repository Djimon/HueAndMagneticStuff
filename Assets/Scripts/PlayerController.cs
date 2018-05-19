using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Range(1,10)]
    public int playerSpeed=5;
    [Range(1, 5000)]
    public int JumpForce=1250;
    public bool facingRight = true;
    float moveX;
    Rigidbody2D body;
    bool isGrounded = true;

    void Awake()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMove();	
	}

    void PlayerMove()
    {
        //Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            Debug.Log("JUMP!!");
        }
        if (Input.GetButtonDown("Activate"))
            Debug.Log("Pressed F");

        //Direction
        if (moveX < 0.0f && facingRight == false)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight==true)
            FlipPlayer();
        //Physics
        body.velocity = new Vector2(moveX * playerSpeed,body.velocity.y);
        //Animation
    }

    void Jump()
    {
        if (isGrounded)
        {
            body.AddForce(Vector2.up * JumpForce);
            isGrounded = false;
        }        
    }

    void FlipPlayer()
    {
        //Change direction
        Debug.Log("Flip....");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Grounded");
        if (col.gameObject.tag == "ground")
            isGrounded = true;
    }

}
