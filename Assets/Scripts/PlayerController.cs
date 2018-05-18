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
        moveX = Input.GetAxis("horizontal");
        //Direction
        if (moveX < 0.0f && facingRight == false)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight==true)
            FlipPlayer();
        //Physics
        gameObject.getComponent<RigidBody2D>().velocity = new Vector2(moveX * playerSpeed,gameObject.getComponent);
        //Animation
    }

    void Jump()
    {

    }

    void FlipPlayer()
    {
        //Change direction
    }

}
