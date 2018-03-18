using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileCantShoot : MonoBehaviour {
	private string contraintName;

	// Use this for initialization
	void Start () {
		contraintName = "Bush_" + GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col) {
		Script_WeaponBase weapon = col.gameObject.GetComponent<Script_WeaponBase>();
		if (!col.isTrigger && weapon) {
			weapon.addContraint(contraintName, false);
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		Script_WeaponBase weapon = col.gameObject.GetComponent<Script_WeaponBase>();
		if (!col.isTrigger && weapon) {
			weapon.removeContraint(contraintName);
		}
	}
}
