using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    void Awake()
    {
        EventManager.StartListening("activateMap", ActivateMap);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    void ActivateMap()
    {
        Debug.Log("Map is activated");

        //should Deactivates itself
    }
}
