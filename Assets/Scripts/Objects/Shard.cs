using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour {

    public Color color;

    private SpriteRenderer spriteRenderer;
    private GameManager gameManger;

    private bool collected = false;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

	// Use this for initialization
	void Start ()
    {
        gameManger = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!collected && other.gameObject.layer == 8)
        {
            gameManger.ShardCollected(color);
            collected = true;
            Destroy(gameObject);
        }
    }

}
