using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectState : MonoBehaviour {

	public virtual void OnEnter() { return; }
	public virtual void OnLeave() { return; }
	public virtual void OnUpdate() { return; }
	public virtual void OnMessage() { return; }
}

