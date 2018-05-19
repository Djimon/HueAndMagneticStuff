using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitch : Switch {

    public float timer = 3.0f;

    bool isCountingDown = false;

    // Use this for initialization
    void Start () {
		
	}

    public void Update()
    {
        if (isON && !isCountingDown)
        {
            isCountingDown = true;
            StartCoroutine("Countdown");
        }

    }

    private IEnumerator Countdown()
    {
        //Debug.Log("--starting Coroutine");
        yield return new WaitForSeconds(timer);
        isCountingDown = false;
        ToggleSwitch();
        //Debug.Log("--Coroutine ended");
    }


	
}
