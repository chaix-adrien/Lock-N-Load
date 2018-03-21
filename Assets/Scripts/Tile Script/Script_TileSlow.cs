using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileSlow : Script_TileHandler {

	public float speedCoef;
	private string contraintName;
	protected override void Start () {
		base.Start();
		contraintName = "mug_" + GetInstanceID();
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

	void OnDestroy() {
		foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
			player.GetComponent<Script_Move>().removeContraint(contraintName);
		}
	}
}
