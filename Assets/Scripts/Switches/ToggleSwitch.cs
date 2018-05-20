using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : Switch {

    public float Switchoffset = 5f;

    private Animator animator;
    // Use this for initialization
    void Start ()
    {
        animator = gameObject.GetComponent<Animator>();	
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        // Layer 8: Player
        if (inRange && Input.GetButtonDown("Activate"))
            Activate();
    }

    public void Activate()
    {
        Debug.Log("activate...");
        ToggleSwitch();
    }

}
