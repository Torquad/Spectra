using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GunMode : int
{
	RadioOpera,
	RadioLullaby,
	RadioMetal,
	Microwave,
	VisibleRed,
	VisibleGreen,
	VisibleBlue,
	Xray,
	GammaRay
}


public class ShootState : MonoBehaviour {
	public GunMode state;
	public Text gunStateText;
	//GameObjectState[] stateList = { RadioState };
	//GunMode state;

	// Use this for initialization
	void Start () {
		state = GunMode.RadioOpera;
		gunStateText.text = state.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown ("e")) //change state to the right
		{
			SetState ((int)state == 8 ? state : state + 1); //can't shift right out of rightmost state
		} 
		else if (Input.GetKeyDown ("q")) //change state to the left
		{
			SetState ((int)state == 0 ? state : state - 1); //can't shift left out of leftmost state
		}

	}

	void SetState(GunMode newState)
	{
		switch (state)
		{
			case GunMode.RadioOpera:
				break;
			case GunMode.RadioLullaby:
				break;
			case GunMode.RadioMetal:
				break;
			case GunMode.Microwave:
				break;
			case GunMode.VisibleRed:
				break;
			case GunMode.VisibleGreen:
				break;
			case GunMode.VisibleBlue:
				break;
			case GunMode.Xray:
				break;
			case GunMode.GammaRay:
				break;
		}


//		switch (newState)
//		{
//
//
//		}
			
		state = newState;
		gunStateText.text = state.ToString ();
	}
}
