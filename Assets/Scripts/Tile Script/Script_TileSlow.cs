using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileSlow : Script_TileHandler {

	public float speedCoef;
	// Use this for initialization
	private string contraintName;
	void Start () {
		contraintName = "mug_" + GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void  walkedOnEnter(Collider2D col) {
		Script_Move moveComp = col.gameObject.GetComponent<Script_Move>();
		if (!col.isTrigger && moveComp) {
			moveComp.addContraint(contraintName, speedCoef);
		}
	}

	protected override void  walkedOnLeave(Collider2D col) {
		Script_Move moveComp = col.gameObject.GetComponent<Script_Move>();
		if (!col.isTrigger && moveComp) {
			moveComp.removeContraint(contraintName);
		}
	}
}
