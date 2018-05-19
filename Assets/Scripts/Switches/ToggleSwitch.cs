using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : Switch {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isON){ /*set sprite on-state*/ }
        else {  /*set sprite off-state*/  }

        // Layer 8: Player
        if (inRange && Input.GetButtonDown("Activate"))
            Activate();
    }

    public void Activate()
    {
        ToggleSwitch();
    }

    
}
