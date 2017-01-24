using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateScript : InteractableObject {
	
	public AudioClip crateDestroy;

	// Use this for initialization
	void Start () {
		Setup ();

		audioSource.clip = crateDestroy;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying && destroyed)
		{
			Destroy (this.gameObject);
		}
	}

	public override void OnEnterGamma ()
	{
		destroyed = true;
		audioSource.Play ();
		rend.enabled = false;
		col.enabled = false;
		SendMessage ("Die");
	}
}
