using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : InteractableObject {
	public AudioClip lullabyClip;
	public AudioClip operaClip;
	public AudioClip metalClip;
	private AudioClip originalClip;
	private float clipTime;

	private Collider2D[] objectsInRange;

	LinkedList<InteractableObject> effectedEnemies;

	// Use this for initialization
	void Start () {
		Setup ();
		effectedEnemies = new LinkedList<InteractableObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnEnterRadioOpera()
	{
		PlayMusic (operaClip, "Opera");
	}

	public override void OnEnterRadioMetal()
	{
		PlayMusic (metalClip, "Metal");
	}

	public override void OnEnterRadioLullaby()
	{
		PlayMusic (lullabyClip, "Lullaby");
	}

	private void PlayMusic(AudioClip clip, string musicType)
	{
		StopCoroutine ("StopAfterDelay");
//		Stop ();
		/////PLAY AUDIO CLIP/////
		if (musicSource.isPlaying && musicSource.clip != clip)
		{
			//dont save the state of another radio clip
			if (musicSource.clip != lullabyClip && musicSource.clip != metalClip && musicSource.clip != operaClip)
			{
				originalClip = musicSource.clip;
				clipTime = musicSource.time;
			}
			musicSource.Stop ();
		}

		if (!musicSource.isPlaying) //dont restart if already playing
		{
			//play lullaby sound
			musicSource.time = 0f;
			musicSource.clip = clip;
			musicSource.Play ();
		}

		/////AFFECT ENEMIES/////
		//select all targets (glass/enemies) in a radius and affect them
		objectsInRange = Physics2D.OverlapCircleAll(transform.position,5);
		foreach (Collider2D target in objectsInRange)
		{
			InteractableObject iO = target.transform.GetComponent<InteractableObject> ();
			if (iO != null)
			{
				effectedEnemies.AddLast (iO);
				iO.SendMessage ("OnMusic" + musicType);
			}
		}
	}

	IEnumerator StopAfterDelay()
	{
		yield return new WaitForSeconds (1.0f);

		/////AFFECT ENEMIES/////
		//select all targets (glass/enemies) in a radius and sleep
		//objectsInRange = Physics2D.OverlapCircleAll(transform.position,5);
		musicSource.Stop();
		musicSource.time = clipTime;
		musicSource.clip = originalClip;
		musicSource.Play ();
		StopCoroutine ("StopAfterDelay");
		Stop ();
	}

	public override void OnLeaveRadio ()
	{
		StopCoroutine ("StopAfterDelay"); //stop other one from running if that is the case
		StartCoroutine ("StopAfterDelay");
	}
	void Stop()
	{
		if (effectedEnemies == null)
			return;
		
		for(LinkedListNode<InteractableObject> iO = effectedEnemies.First; iO != effectedEnemies.Last.Next; iO = iO.Next)
		{
			if (iO != null && iO.Value != null)
			{
				iO.Value.SendMessage ("OnStopMusic");
			}
		}
		effectedEnemies.Clear ();
	}
}
