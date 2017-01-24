using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpectraLib;

public class EnemyScript : InteractableObject {
	//player var
	private Transform player;

	//self vars
	public int health;
	public SecondaryState secondaryState;

	//death vars
	public AudioClip deathSound;
	public AudioClip enemyHurt;

	//sleep sprite
	public Transform sleepSprite;
	private SpriteRenderer sleepSpriteRend;

	//state vars
	public bool grounded = true;

	public enum SecondaryState : int
	{
		Attack,
		Asleep,
		Bounce
	}

	// Use this for initialization
	void Start () {
		Setup ();

		health = -1;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		if (sleepSprite)
		{
			sleepSpriteRend = sleepSprite.GetComponent<SpriteRenderer> ();
		}
	}

	// Update is called once per frame
	void Update () {
		Fade ();

		if (!audioSource.isPlaying && doneFading)
		{
			Destroy (this.gameObject);
		}
	}

	void Die()
	{		
		//yield return new WaitForSeconds(1f);
		audioSource.clip = deathSound;
		audioSource.Play ();

		isFading = true;
		col.enabled = false;
		rb.gravityScale = 0;
	}

	//------------------------- STIMULI FUNCTIONS -------------------------// 
	//------------------------- PRIMARY STATE-BASED BEHAVIOUR -------------------------// 

	//yolo
	public override void OnEnterGamma ()
	{
		audioSource.clip = enemyHurt;
		audioSource.Play ();
		health--;

		if (health <= 0 && !destroyed)
		{
			destroyed = true;
			SendMessage("Die");
		}
	}

	/////////////////////////// SECONDARY STIMULI /////////////////////////// 
	public override void OnMusicLullaby()
	{
		//do art things here
		SetSecondaryState (SecondaryState.Asleep);
		return;
	}
	public override void OnMusicMetal() 
	{
		//do art things here
		SetSecondaryState (SecondaryState.Bounce);
		return;
	}

	public override void OnStopMusic ()
	{
		SetSecondaryState (SecondaryState.Attack);
	}

	public override void OnMetalSparks() 
	{
		audioSource.clip = enemyHurt;
		audioSource.Play ();

		health--;
		if (health <= 0 && !destroyed)
		{
			destroyed = true;
			SendMessage("Die");//Die ();
		}
		return; 
	}

	/////////////////////////// SECONDARY STATE-BASED BEHAVIOUR /////////////////////////// 
	void FixedUpdate () {
		switch (secondaryState)
		{
			case SecondaryState.Attack:
				Attack ();
				break;
			case SecondaryState.Asleep:
				break;
			case SecondaryState.Bounce:
				Bounce ();
				break;
			default:
				throw new UnityException ("BadState");
		}
	}

	/////////////////////////// SET SECONDARY STATES /////////////////////////// 
	void SetSecondaryState(SecondaryState newState)
	{
		switch (secondaryState)
		{
			case SecondaryState.Attack:
				break;
			case SecondaryState.Asleep:
				sleepSpriteRend.enabled = false;
				break;
			case SecondaryState.Bounce:
				break;
			default:
				throw new UnityException ("BadState");
		}

		secondaryState = newState;

		switch (secondaryState)
		{
			case SecondaryState.Attack:
				break;
			case SecondaryState.Asleep:
				sleepSpriteRend.enabled = true;
				break;
			case SecondaryState.Bounce:
				break;
			default:
				throw new UnityException ("BadState");
		}
	}

	//Movement
	void Attack()
	{
		if (grounded)
		{
			Vector3 move = new Vector3 ((player.position.x - transform.position.x), 0, 0);
			move.Normalize ();
			transform.position += move * 3 * Time.deltaTime;

			if (move.x < 0)
			{
				rend.flipX = true;
			} else
			{
				rend.flipX = false;
			}
		}
	}

	void Bounce()
	{
		if (grounded)
		{
			rb.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
			grounded = false;
		}
	}
	/////////////////////////// COLLISION RESET GROUNDED /////////////////////////// 
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.CompareTag("Platform") || coll.collider.CompareTag ("Glass"))
		{
			grounded = true;
		}
	}
}