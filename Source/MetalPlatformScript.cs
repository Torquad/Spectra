using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPlatformScript : InteractableObject {
	public Transform sparks;
	private SpriteRenderer sparksRend;
	public AudioClip sparkSounds;

	private Collider2D[] objectsInRange;
	// Use this for initialization
	void Start () {
		Setup ();
		sparksRend = sparks.GetComponent<SpriteRenderer> ();
		sparksRend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnEnterMicrowave ()
	{
		StopCoroutine ("StopAfterDelay");

		/////PLAY AUDIO CLIP/////
		if (!audioSource.isPlaying) //dont restart if already playing
		{
			//play lullaby sound
			audioSource.clip = sparkSounds;
			audioSource.Play ();
		}

		/////AFFECT ENEMIES/////
		//select all targets (glass/enemies) in a radius and affect them
		sparksRend.enabled = true;
		objectsInRange = Physics2D.OverlapBoxAll (sparks.position, Vector2.one, 0);

		foreach (Collider2D target in objectsInRange)
		{
			InteractableObject iO = target.transform.GetComponent<InteractableObject> ();
			if (iO != null)
			{
				iO.SendMessage ("OnMetalSparks");
			}
		}
	}

	IEnumerator StopAfterDelay()
	{
		yield return new WaitForSeconds (1.0f);

		/////AFFECT ENEMIES/////
		//select all targets (glass/enemies) in a radius and sleep
		audioSource.Stop();
		sparksRend.enabled = false;
		StopCoroutine ("StopAfterDelay");
	}

	public override void OnLeaveMicrowave ()
	{
		StopCoroutine ("StopAfterDelay"); //stop other one from running if that is the case
		StartCoroutine ("StopAfterDelay");
	}
}
