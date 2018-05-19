using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch: MonoBehaviour {


    public bool isON = false;
    public ColorSource target;
    public bool triggerOnTouch = false;
    [HideInInspector]
    public bool isTargetSet = false;
    [HideInInspector]
    public bool inRange = false;

    public void ToggleSwitch()
    {
        isON = !isON;
        if (!isON)
        {
            target.ToggleEmittor();
            isTargetSet = false;
        }
        else
        {
            target.ToggleEmittor();
            isTargetSet = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (triggerOnTouch && col.gameObject.tag == "hasWeight")
        {
            isON = true;
            target.ToggleEmittor();
            isTargetSet = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (triggerOnTouch && col.gameObject.tag == "hasWeight")
        {
            isON = false;
            target.ToggleEmittor();
            isTargetSet = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger enters");
        inRange = true;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        Debug.Log("leaved trigger");
    }


}
