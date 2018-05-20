using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour {


    public LevelManager levelManager;
    // Use this for initialization
    void Start ()
    {
        //levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            levelManager.LoadNextLevel();
        }
    }
}
