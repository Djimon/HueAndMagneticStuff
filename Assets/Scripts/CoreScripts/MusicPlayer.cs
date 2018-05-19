using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	// global instance: if null: create, if not null, dont create = destroy
	void Awake () 
	{
		Debug.Log("player Awake "+GetInstanceID());
		if (instance != null) 
		{
			Destroy(gameObject);
			print ("Duplicated player killed.");
		}
		else
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
