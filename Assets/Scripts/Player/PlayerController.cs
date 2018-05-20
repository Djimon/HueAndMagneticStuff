﻿using System;
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
    public bool isGrounded = true;
    bool setMap = false;

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
        CapVelocity();
	}

    private void CapVelocity()
    {
        Vector2.ClampMagnitude(body.velocity, 1.1f *playerSpeed);
        if (body.velocity.magnitude >= (1.1f * playerSpeed))
        {
            Debug.Log("too fast!!!");
        }
    }

    public void Move(Vector2 movement)
    {
        body.velocity += new Vector2(movement.x * 7.95f * playerSpeed, movement.y);
    }

    void PlayerMove()
    {
        //Controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetButtonDown("Activate"))
            Debug.Log("Pressed F");

        ShowMap();

        //Direction
        if (moveX < 0.0f && facingRight == false)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight == true)
            FlipPlayer();
        //Physics
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);
        //Animation
    }

    void Jump()
    {
        if (isGrounded)
        {
            body.AddForce(Vector2.up * JumpForce);
            isGrounded = false;
            Debug.Log("JUMP!!");
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
        {
            Vector2 vec = col.gameObject.transform.position - gameObject.transform.position;
            float angle = Mathf.Abs(Vector2.Angle(Vector2.down, vec));
            Debug.Log(angle+" < 90 ? ->"+ (angle<89f?true:false));
            isGrounded = angle < 89f ? true : false;
        }
            
    }

    void ShowMap()
    {
        bool ispressed = false;
        //just for testing, will be cutted out later, should be replaced by a interactable object in the world
        if (!setMap && Input.GetKeyDown(KeyCode.M) && !ispressed)
        {
            ispressed = true;
            setMap = true;
            EventManager.TriggerEvent("activateMap");
        }
        else if (setMap && Input.GetKeyDown(KeyCode.M) && !ispressed)
        {
            ispressed = true;
            setMap = false;
            EventManager.TriggerEvent("deactivateMap");
        }

        if (Input.GetKeyUp(KeyCode.M))
            ispressed = false;
    }

}
