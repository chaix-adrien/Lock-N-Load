using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PowerUpAmmo : Script_PowerUp {
	public int ammoToReload = 6;
	protected override bool isUsefull(Collider2D col) {
		Script_WeaponBase weapon = col.gameObject.GetComponentInChildren<Script_WeaponBase>();
		if (weapon)
			return true;
		return false;
	}
	protected override bool use(Collider2D col) {
		Script_WeaponBase weapon = col.gameObject.GetComponent<Script_WeaponBase>();
		if (weapon) {
			weapon.forceReloadWithAmount(ammoToReload);
			base.use(col);
			return true;
		}
		return false;
	}
}
