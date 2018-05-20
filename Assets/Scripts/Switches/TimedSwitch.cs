using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSwitch : Switch {

    public float timer = 3.0f;
    public bool nearEnough = false; 
    bool isCountingDown = false;

    SpriteRenderer renderer;

    // Use this for initialization
    void Start ()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = target.Color;
        renderer.enabled = !target.IsEmitting;
	}

    public void FixedUpdate()
    {
        if(!isCountingDown && inRange && Input.GetButtonDown("Activate"))
            Activate();
    }

    public void Activate()
    {
        Debug.Log("activate...");
        ToggleSwitch();
        renderer.enabled = !target.IsEmitting;
        StartCoroutine("Countdown");
    }

    private IEnumerator Countdown()
    {
        Debug.Log("--starting Coroutine");
        yield return new WaitForSeconds(timer);
        isCountingDown = false;
        ToggleSwitch();
        renderer.enabled =!target.IsEmitting;
        //Debug.Log("--Coroutine ended");
    }


	
}
