using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileChest : Script_Interactible {
	GameObject[] contain;
	// Use this for initialization
	void Start () {
		base.Start();
	}
	public override void interactWhith(GameObject interactor) {
		Debug.Log("Interacted");
	}
}
