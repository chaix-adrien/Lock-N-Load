using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PowerUpInvincible : Script_PowerUp {
	public float invincibleTime = 3.0f;

	protected override bool isUsefull(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (entity)
			return true;
		return false;
	}
	protected override bool use(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (entity) {
			entity.turnInvincible(invincibleTime);
			base.use(col);
			return true;
		}
		return false;
	}
}
