using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour 
{
	public void LoadLevel(string name)
	{
		Debug.Log("level changed to "+name);
		SceneManager.LoadScene(name);
	}
	
	public void LoadNextLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	public void Quit()
	{
		Debug.Log("quit requested");
		Application.Quit(); // bad for web or smartphones
	}

    public void OnPlayerDied()
    {
        Respawn();
    }

    private void Respawn()
    {
        throw new NotImplementedException();
    }
}
