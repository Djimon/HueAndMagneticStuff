using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Range(1,100)]
    public int playerSpeed=5;
    [Range(1000, 5000)]
    public int JumpForce=1250;
    [Range(10,180)]
    float steepSlopeAngle = 60.0f;
    float slopeThreshold = 0.01f;
    float rayDistance = 10.0f;
    public bool facingRight = true;
    float moveX;
    Rigidbody2D body;
    public bool isGrounded = true;
    bool setMap = false;

    private Vector2 desiredVelocity;
    private float slopeRayHeight;
    private Vector2 position;
    private bool movingActivated = true;

    public void SetMovable(bool yes)
    {
        movingActivated = yes;
    }

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
        if (Input.GetButtonDown("Activate"))
            Debug.Log("Pressed F");

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (movingActivated)
        {
            moveX = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        ShowMap();

        //Direction
        if (moveX < 0.0f && facingRight == false)
            FlipPlayer();
        else if (moveX > 0.0f && facingRight == true)
            FlipPlayer();
        //Physics
        body.velocity = new Vector2(moveX * playerSpeed, body.velocity.y);
        if (body.velocity.y >= (1.3f * playerSpeed))
        {
            body.velocity.Set(body.velocity.x, 1.3f * playerSpeed);
            Debug.Log("Vleocity Set");
        }
        desiredVelocity.Set(moveX, body.velocity.y);
        slopeRayHeight = 1f; //TODO: calculate Y offset from the ground you wish to cast your ray from
        position = this.transform.position;

        /*
        if (checkMoveableTerrain(position, new Vector2(desiredVelocity.x, desiredVelocity.y), rayDistance)) // filter the y out, so it only checks forward... could get messy with the cosine otherwise.
        {
            body.velocity = desiredVelocity;
        }
        else
        {
            body.velocity = new Vector2(moveX, 0f);
        }
        */
    }

    bool checkMoveableTerrain(Vector2 position, Vector2 desiredDirection, float distance)
    {
        Ray myRay = new Ray(position, desiredDirection); // cast a Ray from the position of our gameObject into our desired direction. Add the slopeRayHeight to the Y parameter.

        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit, distance))
        {
            if (hit.collider.gameObject.tag == "ground") // Our Ray has hit the ground
            {
                float slopeAngle = Mathf.Deg2Rad * Vector3.Angle(Vector3.up, hit.normal); // Here we get the angle between the Up Vector and the normal of the wall we are checking against: 90 for straight up walls, 0 for flat ground.

                float radius = Mathf.Abs(slopeRayHeight / Mathf.Sin(slopeAngle)); // slopeRayHeight is the Y offset from the ground you wish to cast your ray from.

                if (slopeAngle >= steepSlopeAngle * Mathf.Deg2Rad) //You can set "steepSlopeAngle" to any angle you wish.
                {
                    if (hit.distance - GetComponent<CircleCollider2D>().radius > Mathf.Abs(Mathf.Cos(slopeAngle) * radius) + slopeThreshold) // Magical Cosine. This is how we find out how near we are to the slope / if we are standing on the slope. as we are casting from the center of the collider we have to remove the collider radius.
                                                                                                                                             // The slopeThreshold helps kills some bugs. ( e.g. cosine being 0 at 90° walls) 0.01 was a good number for me here
                    {
                        return true; // return true if we are still far away from the slope
                    }

                    return false; // return false if we are very near / on the slope && the slope is steep
                }

                return true; // return true if the slope is not steep
            }
            else return true;
        }
        else return true;
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
        if (!isGrounded && (col.gameObject.tag == "ground" || col.gameObject.tag == "hasWeight"))
        {
            Vector2 vec = col.gameObject.transform.position - gameObject.transform.position;
            float angle = Mathf.Abs(Vector2.Angle(Vector2.down, vec));
            //Debug.Log(angle+" < 90 ? ->"+ (angle<89f?true:false));
            isGrounded = angle < 90f ? true : false;
            Debug.Log("Grounded");
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
