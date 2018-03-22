using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PowerUpLife : Script_PowerUp {
	public int lifeGiven = 40;
	
	protected override bool isUsefull(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (entity && entity.getPercentLife() < 1f)
			return true;
		return false;
	}
	protected override bool use(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (entity) {
			entity.heal(lifeGiven);
			base.use(col);
			return true;
		}
		return false;
	}
}
