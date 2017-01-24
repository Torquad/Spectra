using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectColor
{
	None,
	Red,
	Green,
	Blue
}

public class ColoredObject : InteractableObject {
	
	public ObjectColor color = ObjectColor.Red;

	// Use this for initialization
	void Start () {
		Setup ();
		rend.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnEnterVisibleRed()
	{
		if (color == ObjectColor.Red)
		{
			rend.enabled = true;
		}
	}

	public override void OnEnterVisibleGreen()
	{
		if (color == ObjectColor.Green)
		{
			rend.enabled = true;
		}
	}

	public override void OnEnterVisibleBlue()
	{
		if (color == ObjectColor.Blue)
		{
			rend.enabled = true;
		}
	}

	public override void OnLeaveVisibleRed()
	{
		if (color == ObjectColor.Red)
		{
			rend.enabled = false;
		}
	}

	public override void OnLeaveVisibleGreen()
	{
		if (color == ObjectColor.Green)
		{
			rend.enabled = false;
		}
	}

	public override void OnLeaveVisibleBlue()
	{
		if (color == ObjectColor.Blue)
		{
			rend.enabled = false;
		}
	}
}
