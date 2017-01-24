using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpectraLib;

public class InteractableObject : MonoBehaviour {
	
	//fade vars
	protected bool isFading = false;
	protected bool doneFading = false;
	protected float currentTime = 0;
	protected float timeReq = 1;
	protected bool destroyed = false;

	//self vars
	protected AudioSource audioSource;
	protected AudioSource musicSource;
	protected BoxCollider2D col;
	protected Rigidbody2D rb;
	protected SpriteRenderer rend;

	protected void Setup()
	{
		audioSource = GetComponent<AudioSource> ();
		musicSource = GameObject.FindWithTag ("Menu").GetComponent<AudioSource> ();
		col = GetComponent<BoxCollider2D> ();
		rb = GetComponent<Rigidbody2D> ();
		rend = GetComponent<SpriteRenderer> ();

	}
	protected void Fade()
	{
		if (isFading)
		{
			currentTime = SpecLib.Fade (currentTime, timeReq, rend);
			if (currentTime >= timeReq)
			{
				doneFading = true;
			}
		}
	}

	public enum PrimaryState
	{
		None,
		RadioOpera,
		RadioLullaby,
		RadioMetal,
		Microwave,
		VisibleRed,
		VisibleGreen,
		VisibleBlue,
		Xray,
		Gamma
	}

	PrimaryState primaryState;

	//Primary stimuli
	public void OnRadioOpera() { SetPrimaryState(PrimaryState.RadioOpera); }
	public void OnRadioLullaby() { SetPrimaryState(PrimaryState.RadioLullaby); }
	public void OnRadioMetal() { SetPrimaryState(PrimaryState.RadioMetal); }
	public void OnMicrowave() { SetPrimaryState(PrimaryState.Microwave); }
	public void OnVisibleRed() { SetPrimaryState(PrimaryState.VisibleRed); }
	public void OnVisibleGreen() { SetPrimaryState(PrimaryState.VisibleGreen); }
	public void OnVisibleBlue() { SetPrimaryState(PrimaryState.VisibleBlue); }
	public void OnXray() { SetPrimaryState(PrimaryState.Xray); }
	public void OnGammaRay() { SetPrimaryState(PrimaryState.Gamma); }

	public void OnStopUse() { SetPrimaryState(PrimaryState.None); } //player has stopped directly shooting object

	//Secondary stimuli
	public virtual void OnMusicOpera() { return; }
	public virtual void OnMusicLullaby() { return; }
	public virtual void OnMusicMetal() { return; }
	public virtual void OnMetalSparks() { return; }

	public virtual void OnStopMusic() { return; } //music effect has ceased

	//State functions
	//Enter state
	public virtual void OnEnterRadioOpera() { return; }
	public virtual void OnEnterRadioLullaby() { return; }
	public virtual void OnEnterRadioMetal() { return; }
	public virtual void OnEnterMicrowave() { return; }
	public virtual void OnEnterVisibleRed() { return; }
	public virtual void OnEnterVisibleGreen() { return; }
	public virtual void OnEnterVisibleBlue() { return; }
	public virtual void OnEnterXray () 
	{
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		Color tmp = rend.color;
		tmp.a = 0.5f;
		rend.color = tmp;
	}
	public virtual void OnEnterGamma () { return; }

	//Leave state
	public virtual void OnLeaveRadio() { return; }
	//public virtual void OnLeaveRadioOpera() { return; }
	//public virtual void OnLeaveRadioLullaby() { return; }
	//public virtual void OnLeaveRadioMetal() { return; }
	public virtual void OnLeaveMicrowave() { return; }
	public virtual void OnLeaveVisibleRed() { return; }
	public virtual void OnLeaveVisibleGreen() { return; }
	public virtual void OnLeaveVisibleBlue() { return; }
	public virtual void OnLeaveXray() 
	{ 
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		Color tmp = rend.color;
		tmp.a = 1f;
		rend.color = tmp; 
	}
	public virtual void OnLeaveGamma() { return; }

	public virtual void SetPrimaryState(PrimaryState newState)
	{
		switch (primaryState) //leaving state
		{
			case PrimaryState.None:
				break;

			case PrimaryState.RadioOpera:
				OnLeaveRadio();
				break;
			case PrimaryState.RadioLullaby:
				OnLeaveRadio();
				break;
			case PrimaryState.RadioMetal:
				OnLeaveRadio();
				break;

			case PrimaryState.Microwave:
				OnLeaveMicrowave();
				break;

			case PrimaryState.VisibleRed:
				OnLeaveVisibleRed();
				break;

			case PrimaryState.VisibleGreen:
				OnLeaveVisibleGreen();
				break;

			case PrimaryState.VisibleBlue:
				OnLeaveVisibleBlue();
				break;

			case PrimaryState.Xray:
				OnLeaveXray();
				break;

			case PrimaryState.Gamma:
				OnLeaveGamma ();
				break;

			default:
				throw new UnityException ("BadState");
		}

		primaryState = newState;

		switch (primaryState) //leaving state
		{
			case PrimaryState.None:
				break;

			case PrimaryState.RadioOpera:
				OnEnterRadioOpera();
				break;

			case PrimaryState.RadioLullaby:
				OnEnterRadioLullaby();
				break;

			case PrimaryState.RadioMetal:
				OnEnterRadioMetal();
				break;

			case PrimaryState.Microwave:
				OnEnterMicrowave();
				break;

			case PrimaryState.VisibleRed:
				OnEnterVisibleRed();
				break;

			case PrimaryState.VisibleGreen:
				OnEnterVisibleGreen();
				break;

			case PrimaryState.VisibleBlue:
				OnEnterVisibleBlue();
				break;

			case PrimaryState.Xray:
				OnEnterXray();
				break;

			case PrimaryState.Gamma:
				OnEnterGamma ();
				break;

			default:
				throw new UnityException ("BadState");
		}
	}

}
