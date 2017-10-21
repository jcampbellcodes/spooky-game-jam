using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour {

	public GameObject player;       //Public variable to store a reference to the player game object

	// Use this for initialization
	void Start () 
	{
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject == player)
		{
			Debug.Log("won game");
			SceneManager.LoadScene (0);
		}
	}

	void OnTriggerExit(Collider other)
	{
		// Destroy everything that leaves the trigger
		//Destroy(other.gameObject);
	}
}
