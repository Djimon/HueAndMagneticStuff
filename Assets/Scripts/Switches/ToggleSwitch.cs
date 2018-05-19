using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : Switch {

    public float Switchoffset = 5f;
    private bool isSwitched = false;
    // Use this for initialization
    void Start ()
    {
		
	}

	// Update is called once per frame
	void FixedUpdate ()
    {
        

        if (isON && !isSwitched)
        { /*set sprite on-state*/
            isSwitched = true;
        }
        else if (!isON && isSwitched)
        {  /*set sprite off-state*/
            isSwitched = false;
        }

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
