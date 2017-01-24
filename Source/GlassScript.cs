using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : InteractableObject {
	public AudioClip shatterSound;

	bool broken = false;

	// Use this for initialization
	void Start () {
		Setup ();

		audioSource.clip = shatterSound;
	}
	
	// Update is called once per frame
	void Update () {
		Fade ();

		if (!audioSource.isPlaying && doneFading)
		{
			Destroy (this.gameObject);
		}
	}

	public override void OnMusicOpera()
	{
		if (!broken)
		{
			broken = true;
			StartCoroutine("Break");
		}
	}

	IEnumerator Break()
	{
		yield return new WaitForSeconds(1f);
		rend.enabled = false;
		col.enabled = false;
		audioSource.Play ();
	}
}
