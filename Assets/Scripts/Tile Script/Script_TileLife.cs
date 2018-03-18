using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileLife : Script_TileHandler {
	public int lifeGiven = 40;
	public bool onlyToPlayer = true;
	public bool destroyOnUse = true;
	
	public bool useIfUseless = false;
	protected override void  walkedOnEnter(Collider2D col) {
		Debug.Log("walk");
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (!col.isTrigger && entity) {
			if (!onlyToPlayer || col.tag == "Player") {
				if (useIfUseless || entity.getPercentLife() < 1f) {
					entity.heal(lifeGiven);
					if (destroyOnUse) {
						Destroy(gameObject);
					}
				}
			}
		}
	}
}
