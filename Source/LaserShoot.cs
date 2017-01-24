using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum GunMode
//{
//	RadioOpera,
//	RadioLullaby,
//	RadioMetal,
//	Microwave,
//	VisibleRed,
//	VisibleGreen,
//	VisibleBlue,
//	Xray,
//	GammaRay
//}

public class LaserShoot : MonoBehaviour {
	//reference to gun state machine
	public ShootState shootState;

	//player vars
	public Transform playerTransform;
	private SpriteRenderer playerSprite;
	public Camera cam;
	private bool facingLeft = false;
	private RaycastHit2D hit;
	private AudioSource audioSource;

	//Shoot SFX

	public AudioClip blueLight;
	public AudioClip gammaRay;
	public AudioClip greenLight;
	public AudioClip microwave;
	public AudioClip redLight;
	public AudioClip radio;

	public LayerMask mask;

	//general gun vars
	private Transform gunTransform;

	//shooting
	public LineRenderer laserLine;
	private Vector3 shootTargetPoint;
	InteractableObject shootObject;
	InteractableObject oldObject;

	//visible light vars
	Light visibleFlashlight;

	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer> ();
		gunTransform = GetComponent<Transform> ();
		playerSprite = playerTransform.GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{
		shootTargetPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		shootTargetPoint.z = 0f;

		if (shootTargetPoint.x < playerTransform.position.x && !facingLeft)
		{
			facingLeft = true;
			playerSprite.flipX = true;

			gunTransform.RotateAround (playerTransform.position, Vector3.up, 180f);

		} else if (shootTargetPoint.x > playerTransform.position.x && facingLeft)
		{
			facingLeft = false;
			playerSprite.flipX = false;

			gunTransform.RotateAround (new Vector3 (playerTransform.position.x,0f,0f), Vector3.up, 180f);
		}

		if (Input.GetButton ("Fire1"))
		{		
			PlayLaserSound ();
			//rayOrigin = gunPosition;
			laserLine.SetPosition (0, gunTransform.position);

			hit = Physics2D.Raycast(gunTransform.position, shootTargetPoint-gunTransform.position, 50, mask);

			if (hit.collider != null) //hit something?
			{
				//show laser hitting it
				laserLine.SetPosition (1, hit.point);

				//activate object
				shootObject = hit.transform.GetComponent<InteractableObject> ();
				
				if (shootObject != null)
				{
					//based on gun setting, call appropriate method
					shootObject.SendMessage ("On" + shootState.state.ToString (), 0);
				}
					
				//deactivate old object
				if (shootObject != oldObject)
				{
					DeactivateOldObject ();
				}

				oldObject = shootObject;

			} else
			{
				laserLine.SetPosition (1, shootTargetPoint);
				DeactivateOldObject ();
			}

			laserLine.enabled = true;
		} 
		else
		{
			audioSource.Stop ();
			laserLine.enabled = false;
			DeactivateOldObject ();
		}
	}

	void DeactivateOldObject()
	{
		if (oldObject != null)
		{
			oldObject.SendMessage("OnStopUse");
			oldObject = null;
		}
	}

	void PlayLaserSound()
	{
		AudioClip nextClip;
		switch (shootState.state)
		{
			case GunMode.RadioMetal:
			case GunMode.RadioLullaby:
			case GunMode.RadioOpera:
				nextClip = radio;
				break;
			case GunMode.VisibleBlue:
				nextClip = blueLight;
				break;
			case GunMode.GammaRay:
				nextClip = gammaRay;
				break;
			case GunMode.VisibleGreen:
				nextClip = greenLight;
				break;
			case GunMode.Microwave:
				nextClip = microwave;
				break;
			case GunMode.VisibleRed:
				nextClip = redLight;
				break;
			default:
				nextClip = radio;
				break;
		}

		if (audioSource.isPlaying && audioSource.clip == nextClip)
			return;
		audioSource.clip = nextClip;
		audioSource.Play ();
	}
}
