using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public GameObject mapBackground;
    private Switch[] switches;
    private PlayerController player;
    private Camera camera;

    private Dictionary<Color,int> winCondition;


    void Awake()
    {
        EventManager.StartListening("activateMap", ActivateMap);
        EventManager.StartListening("deactivateMap", DeactivateMap);
        winCondition = new Dictionary<Color, int>();  
    }

    // Use this for initialization
    void Start ()
    {
        switches = FindObjectsOfType<Switch>();
        player = FindObjectOfType<PlayerController>();
        camera = FindObjectOfType<Camera>();
        if (player != null)
            Debug.Log("Player found");

        InitializeShards();
    }

    private void InitializeShards()
    {
        Shard[] shards = FindObjectsOfType<Shard>();
        for (int i = 0; i < shards.Length; i++)
        {
            Color temp = shards[i].color;
            if (winCondition.ContainsKey(temp))
                winCondition[temp]++;
            else
                winCondition.Add(temp, 1);
        }

        foreach (Color c in winCondition.Keys)
        {
            Debug.Log(c.ToColorName() + " = " + winCondition[c]);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
	}

    public bool CheckWinCondition()
    {
        int rest = 0;
        foreach (Color c in winCondition.Keys)
        {
            rest += winCondition[c];
        }

        if (rest <= 0)
            return true;
        else
            return false;
    }

    public void ShardCollected(Color color)
    {
        Debug.Log("Shard collected: "+color.ToColorName());
        winCondition[color]--;

    }

    void ActivateMap()
    {
        Debug.Log("Map is activated");
        //show Linerenderer of each Switch
        mapBackground.transform.position = player.transform.position;
        mapBackground.SetActive(true);
        camera.orthographicSize += 2f;
        player.SetMovable(false);
        foreach (Switch s in switches)
        {
            s.GetComponent<LineRenderer>().enabled = true;
            //show all Icons of each switch
            for (int i = 0; i < s.transform.childCount; i++)
            {
                GameObject icon = s.transform.GetChild(i).gameObject;
                if (icon.tag == "icon")
                    icon.SetActive(true);
            }
        }
    }

    void DeactivateMap()
    {
        Debug.Log("Map is deactivated");
        //show Linerenderer of each Switch
        mapBackground.SetActive(false);
        camera.orthographicSize -= 2f;
        player.SetMovable(true);
        foreach (Switch s in switches)
        {
            s.GetComponent<LineRenderer>().enabled = false;
            for (int i = 0; i < s.transform.childCount; i++)
            {
                GameObject icon = s.transform.GetChild(i).gameObject;
                if (icon.tag == "icon")
                    icon.SetActive(false);
            }
        }
    }
}
