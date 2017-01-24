using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript: InteractableObject {

	//self vars
	public int health = 3;
	public int numKeys;
	float speed = 10f;
	float jumpSpeed = 0.0015f;
	bool jump = false;
	bool grounded = true;
	Vector3 moveDirection;

	//Music
	public AudioClip normalMusic;
	public AudioClip lowHealthMusic;
	public AudioClip gameOverMusic;

	//SFX
	public AudioClip chickenEat;
	public AudioClip takeKey;
	public AudioClip playerHurt;
	public AudioClip doorUse;
	public AudioClip doorDeny;
	
	bool gameOver = false;

	void Start () {
		Setup ();

		rb.gravityScale = 2;
	}

	// Update is called once per frame
	void FixedUpdate () {
		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

		if (Input.GetKeyDown ("space") && grounded)
		{
			jump = true;
		}
	}

	void Update()
	{
		if (jump)
		{
			rb.AddForce (new Vector2 (0, jumpSpeed), ForceMode2D.Impulse);
			jump = false;
			grounded = false;
		}

		transform.Translate(moveDirection * speed * Time.deltaTime);
		if (musicSource != null) {
			if (gameOver && musicSource.isPlaying == false) {
				SceneManager.LoadScene ("0_title");
			}
		} else if (gameOver) {
			SceneManager.LoadScene ("0_title");
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (other.CompareTag ("Key")) 
		{
			audioSource.clip = takeKey;
			audioSource.Play ();

			numKeys++;
			other.GetComponent<KeyScript> ().Take ();
		} 
		else if (other.CompareTag ("Door")) 
		{
			if (numKeys > 0)
			{
				audioSource.clip = doorUse;
				audioSource.Play ();
				numKeys--;
				other.GetComponent<DoorScript> ().Use ();
			} else
			{
				audioSource.clip = doorDeny;
				audioSource.Play ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D other = col.collider;
		if (other.CompareTag ("Key")) 
		{
			audioSource.clip = takeKey;
			audioSource.Play ();

			numKeys++;
			other.SendMessage("Take");
		}
		else if (other.CompareTag ("Food")) 
		{
			audioSource.clip = chickenEat;
			audioSource.Play ();

			if (health == 1) { //health was low
				if (musicSource != null) {
					musicSource.clip = normalMusic;
					musicSource.Play ();
				}
			}

			health = health < 3 ? health + 1 : health;
			Destroy (other.gameObject);
		} 
		else if (other.CompareTag ("Platform") || other.CompareTag ("Glass"))
		{
			grounded = true;
		} 
		else if(other.CompareTag ("Enemy"))
		{
			EnemyScript enemy = other.GetComponent<EnemyScript> ();

			if (enemy.secondaryState == EnemyScript.SecondaryState.Attack)
			{
				audioSource.clip = playerHurt;
				audioSource.Play ();
				health--;

				if (health == 1) //health is now low
				{
					if (musicSource != null) {
						musicSource.clip = lowHealthMusic;
						musicSource.Play ();
					}
				}

				if (health <= 0)
				{
					GameOver ();
				}
			}
		}
	}

	void GameOver()
	{
		if (musicSource != null) {
			musicSource.clip = gameOverMusic;
			musicSource.volume = 1f;
			musicSource.loop = false;
			musicSource.Play ();
		}
		gameOver = true;
		Time.timeScale = 0;
	}
}
