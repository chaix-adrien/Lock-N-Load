using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileCantShoot : Script_TileHandler {
	private string contraintName;

	protected override void Start () {
		base.Start();
		contraintName = "Bush_" + GetInstanceID();
	}

	protected override void  walkedOnEnter(Collider2D col) {
		Script_WeaponBase weapon = col.gameObject.GetComponent<Script_WeaponBase>();
		if (!col.isTrigger && weapon) {
			weapon.addContraint(contraintName, false);
		}
	}
	protected override void  walkedOnLeave(Collider2D col) {
		Script_WeaponBase weapon = col.gameObject.GetComponent<Script_WeaponBase>();
		if (!col.isTrigger && weapon) {
			weapon.removeContraint(contraintName);
		}
	}

	void OnDestroy() {
		foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
			player.GetComponent<Script_WeaponBase>().removeContraint(contraintName);
		}
	}
}
