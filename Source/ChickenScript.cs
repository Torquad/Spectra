using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpectraLib;

public class ChickenScript : InteractableObject {
	public bool cooked = false;
	public Sprite cookedChicken;

	public AudioClip chickenCooking;
	public AudioClip eatChicken;

	// Use this for initialization
	void Start () {
		Setup ();

		audioSource.clip = chickenCooking;
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying && destroyed)
		{
			Destroy (this.gameObject);
		}
			
	}


	//Microwave
	public override void OnEnterMicrowave ()
	{
		if (!cooked)
		{
			cooked = true;
			StartCoroutine ("Cook");//Wait ();
		}
	}

	IEnumerator Cook()
	{
		yield return new WaitForSeconds(1f);
		this.gameObject.tag = "Food";
		rend.sprite = cookedChicken;
		audioSource.Play ();
	}


	//Gamma Ray (Kill)
	public override void OnEnterGamma ()
	{
		destroyed = true;
		audioSource.Play ();
		rend.enabled = false;
		col.enabled = false;
		SendMessage ("Die");
	}



	void Die()
	{
		audioSource.Play ();

		isFading = true;
		col.enabled = false;
		rb.gravityScale = 0;
	}


}
