using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    Animator animator;

    public bool touching = false;

	// Use this for initialization
	void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "hasWeight")
        {
            animator.SetTrigger("isPressed");
            touching = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "hasWeight")
        {
            animator.SetTrigger("isReleased");
            touching = false;
        }
    }
}
