using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Text Hinttext;

	// Use this for initialization
	void Start ()
    {
        Hinttext.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            Hinttext.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            Hinttext.enabled = false;
        }
    }

}
