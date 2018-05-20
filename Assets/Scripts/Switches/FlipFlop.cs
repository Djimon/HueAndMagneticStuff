using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipFlop : ToggleSwitch {

    public Switch ListenedSwitch;

    private bool[] prevStates = new bool[] { false, false };

    // Use this for initialization
	void Start ()
    {
        this.isON = !ListenedSwitch.isON;
        this.isTargetSet = !ListenedSwitch.isTargetSet;
        prevStates[0] = ListenedSwitch.isON;
        prevStates[1] = ListenedSwitch.isTargetSet;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (ListenedSwitch.isON != prevStates[0] && ListenedSwitch.isTargetSet != prevStates[1])
        {
            Debug.Log("states different: "+ListenedSwitch.isON);
            ToggleSwitch();
            prevStates[0] = ListenedSwitch.isON;
            prevStates[1] = ListenedSwitch.isTargetSet;
        }
	}
}
