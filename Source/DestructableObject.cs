using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : InteractableObject {

	public override void OnEnterGamma ()
	{
		Destroy(this.gameObject);
	}
}
