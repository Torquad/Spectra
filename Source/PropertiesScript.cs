using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for dynamically adding custom script components to game objects
public class PropertiesScript : MonoBehaviour {
	//dropdown menus
	public ObjectColor color;
	public bool container;
	public AudioClip containerRevealSound;

	//script references
	private ColoredObject coloredObject;

	private ContainerObject containerObject;


	// Use this for initialization
	void Start () {
		if (color != ObjectColor.None)
		{
			coloredObject = this.gameObject.AddComponent<ColoredObject> ();
			coloredObject.color = color;
		}
		if (container)
		{
			containerObject = this.gameObject.AddComponent<ContainerObject> ();
			containerObject.reveal = containerRevealSound;
		}

		//remove properties script at the end
		Destroy(this);
	}

	// Update is called once per frame
	void Update () {

	}
}
