using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerObject : InteractableObject {
	//possible internal objects
	//bool dead = false;
	private Transform internalObject;
	private InteractableObject internalXray;

	public AudioClip reveal;

	// Use this for initialization
	void Start () {
		Setup ();

		foreach (Transform child in transform)
		{
			//only supports one internal object
			if (child.CompareTag ("Key") || child.CompareTag ("Enemy") || child.CompareTag ("Chicken") || child.CompareTag ("Food"))
			{
				internalObject = child;
			}
		}

		if (internalObject != null)
		{
			audioSource = internalObject.GetComponent<AudioSource> ();
			col = internalObject.GetComponent<BoxCollider2D> ();
			rb = internalObject.GetComponent<Rigidbody2D> ();
			rend = internalObject.GetComponent<SpriteRenderer> ();
			internalXray = internalObject.GetComponent<InteractableObject> ();
		}

		Hide ();
		col.enabled = false;
		rb.gravityScale = 0;
	}

	// Update is called once per frame
	void Update () {
		//should find another way to do this
		if (!destroyed)
		{
			internalObject.position = this.transform.position;
		}
	}

	public override void OnEnterXray ()
	{
		Show ();
		if(internalXray != null)
		{
			internalXray.SendMessage ("OnXray");
		}
	}

	public override void OnLeaveXray ()
	{
		Hide ();
		if(internalXray != null)
		{
			internalXray.SendMessage ("OnStopUse");
		}
	}

	public void Die() //attached enemy/crate has died
	{
		destroyed = true;
		Drop ();
	}

	void Show()
	{
		rend.enabled = true;
	}

	void Hide()
	{
		rend.enabled = false;
	}

	public void Drop()
	{
		if (reveal != null)
		{
			audioSource.clip = reveal;
			audioSource.Play ();
		}

		internalObject.parent = null;

		Show ();
		col.enabled = true;
		rb.gravityScale = 1;
		rend.enabled = true;
	}
}
