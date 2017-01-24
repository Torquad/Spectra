using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : InteractableObject {
	private bool taken = false;

	// Use this for initialization
	void Start () {
		Setup ();
	}
	
	// Update is called once per frame
	void Update () {
		if (taken && !audioSource.isPlaying) //don't destroy until done playing reveal sound
		{
			Destroy (this.gameObject);
		}
	}

	public void Take()
	{
		taken = true;
		rend.enabled = false;
	}
}
