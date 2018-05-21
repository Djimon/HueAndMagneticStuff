using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour {

 
    public bool winCondition = false;
    public Text hintText;
    public LevelManager levelManager;
    public GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (gameManager.CheckWinCondition())
                levelManager.LoadNextLevel();
            else
            {
                hintText.text = "Collect all shards!";
                hintText.enabled = true;
            }     
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            hintText.text = "";
            hintText.enabled = false;
        }
    }
}
