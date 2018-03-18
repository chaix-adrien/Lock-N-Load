using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PowerUpShield : Script_PowerUp {

	public int shieldStack = 1;

	public bool refullTotal = true;
	public bool onlyToPlayer = true;
	public bool destroyOnUse = true;
	
	public bool useIfUseless = false;
	protected override void  walkedOnEnter(Collider2D col) {
		Script_Shield shield = col.gameObject.GetComponentInChildren<Script_Shield>();
		if (!col.isTrigger && shield) {
			if ((onlyToPlayer && col.tag == "Player") || !onlyToPlayer) {
				if (useIfUseless || shield.getPercentShield() < 1f) {
					shield.refull(refullTotal ? -1 : shieldStack);
					if (destroyOnUse) {
						Destroy(gameObject);
					}
				}
			}
		}
	}
}
