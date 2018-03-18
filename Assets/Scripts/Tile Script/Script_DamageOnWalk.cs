using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DamageOnWalk : Script_TileHandler {
	public int damages = 10;
	public Color onHitColor = Color.white;
	
	protected override void walkedOnStay(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (!col.isTrigger && entity) {
			entity.hit(damages, onHitColor, "environement");
		}
	}	
}
