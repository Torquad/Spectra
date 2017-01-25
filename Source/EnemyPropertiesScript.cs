using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SecondaryState : int
{
	Attack,
	Asleep,
	Bounce
}

public class EnemyPropertiesScript : InteractableObject {
	public SecondaryState secondaryState;

	public bool soundproof = false;
	public bool radioactive = false;

	//sleep sprite
	private Transform sleepSprite;
	private SpriteRenderer sleepSpriteRend;

	// Use this for initialization
	void Start () {
		Setup ();


		if (!soundproof)
		{
			sleepSprite = Instantiate (GameObject.FindWithTag ("SleepSprite"), this.transform).transform;
			sleepSprite.transform.position = this.transform.position + new Vector3 (0f, 2f, 0f);
			sleepSpriteRend = sleepSprite.GetComponent<SpriteRenderer> ();
		}
		//else add headphones
	}

	// Update is called once per frame
	void Update () {

	}
	//------------------------- STIMULI FUNCTIONS -------------------------// 
	//------------------------- PRIMARY STATE-BASED BEHAVIOUR -------------------------// 

	public override void OnEnterGamma ()
	{
		if (!radioactive)
		{
			SendMessage ("TakeDamage",1);
		}
	}
		
	////////////////////////////// SECONDARY STIMULI /////////////////////////// 
	public override void OnMusicLullaby()
	{
		//do art things here
		if (!soundproof)
		{
			SetSecondaryState (SecondaryState.Asleep);
		}
		return;
	}
	public override void OnMusicMetal() 
	{
		//do art things here
		if (!soundproof)
		{
			SetSecondaryState (SecondaryState.Bounce);
		}
		return;
	}

	public override void OnStopMusic ()
	{
		if (!soundproof)
		{
			SetSecondaryState (SecondaryState.Attack);
		}
	}

	public override void OnMetalSparks() 
	{
		SendMessage ("TakeDamage",3);
	}

	/////////////////////////// SET SECONDARY STATES /////////////////////////// 
	public virtual void OnAttackEnter() { return; }
	public virtual void OnAsleepEnter() { sleepSpriteRend.enabled = true; }
	public virtual void OnBounceEnter() { return; }

	public virtual void OnAttackLeave() { return; }
	public virtual void OnAsleepLeave() { sleepSpriteRend.enabled = false; }
	public virtual void OnBounceLeave() { return; }

	void SetSecondaryState(SecondaryState newState)
	{
		switch (secondaryState)
		{
			case SecondaryState.Attack:
				OnAttackLeave ();
				break;
			case SecondaryState.Asleep:
				OnAsleepLeave ();
				break;
			case SecondaryState.Bounce:
				OnBounceLeave ();
				break;
			default:
				throw new UnityException ("BadState");
		}

		secondaryState = newState;

		switch (secondaryState)
		{
			case SecondaryState.Attack:
				OnAttackEnter ();
				break;
			case SecondaryState.Asleep:
				OnAsleepEnter ();
				break;
			case SecondaryState.Bounce:
				OnBounceEnter ();
				break;
			default:
				throw new UnityException ("BadState");
		}
	}


}