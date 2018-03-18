using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PowerUpShield : Script_PowerUp {
	public int shieldStack = 1;
	public bool refullTotal = true;
	
	protected override bool isUsefull(Collider2D col) {
		Script_Shield shield = col.gameObject.GetComponentInChildren<Script_Shield>();
		if (shield && shield.getPercentShield() < 1f)
			return true;
		return false;
	}

	protected override bool use(Collider2D col) {
		Script_Shield shield = col.gameObject.GetComponentInChildren<Script_Shield>();
		if (shield) {
			shield.refull(refullTotal ? -1 : shieldStack);
			return true;
		}
		return false;
	}
}
