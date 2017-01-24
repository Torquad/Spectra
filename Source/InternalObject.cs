//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//[RequireComponent (typeof(AudioSource))]
//[RequireComponent (typeof(BoxCollider2D))]
//[RequireComponent (typeof(Rigidbody2D))]
//[RequireComponent (typeof(SpriteRenderer))]
//
//
//public class InternalObject : MonoBehaviour {
//	private AudioSource audioSource;
//	private BoxCollider2D col;
//	private Rigidbody2D rb;
//	private SpriteRenderer rend;
//
//
//	public AudioClip revealSound;
//
//	// Use this for initialization
//	void Start () {
//		audioSource = GetComponent<AudioSource> ();
//		col = GetComponent<BoxCollider2D> ();
//		rb = GetComponent<Rigidbody2D> ();
//		rend = GetComponent<SpriteRenderer> ();
//
//		if (this.transform.parent != null)
//		{
//			Hide ();
//			col.enabled = false;
//			rb.gravityScale = 0;
//		}
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		//should find another way to do this
//		if (this.transform.parent != null)
//		{
//			this.transform.position = this.transform.parent.position;
//		}
//	}
//
//	public void Show()
//	{
//		rend.enabled = true;
//	}
//
//	public void Hide()
//	{
//		rend.enabled = false;
//	}
//
//	public void Drop()
//	{
//		audioSource.clip = revealSound;
//		audioSource.Play ();
//
//		this.transform.parent = null;
//
//		Show ();
//		col.enabled = true;
//		rb.gravityScale = 1;
//
//	}
//}
