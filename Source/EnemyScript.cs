using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpectraLib;

public class EnemyScript : InteractableObject {
	//player var
	private Transform player;

	[HideInInspector]
	public SecondaryState secondaryState;
	//self vars
	public int health;

	//death vars
	public AudioClip enemyHurt;
	public AudioClip deathSound;

	//state vars
	public bool grounded = true;

	// Use this for initialization
	void Start () {
		Setup ();

		//health = 3;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
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

	/////////////////////////// SECONDARY STATE-BASED BEHAVIOUR /////////////////////////// 
	void FixedUpdate () {
		secondaryState = transform.GetComponent<EnemyPropertiesScript> ().secondaryState;
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

	//Movement
	void Attack()
	{
		if (grounded)
		{
			Vector3 move = new Vector3 ((player.position.x - transform.position.x), 0, 0);
			if (move.x < -0.5)
			{
				rend.flipX = true;
			} else if (move.x > 0.5)
			{
				rend.flipX = false;
			}

			move.Normalize ();
			transform.position += move * 3 * Time.deltaTime;
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

	void TakeDamage(int damage)
	{
		audioSource.clip = enemyHurt;
		audioSource.Play ();
		health -= damage;

		if (health <= 0 && !destroyed)
		{
			destroyed = true;
			SendMessage("Die");
		}
	}
}