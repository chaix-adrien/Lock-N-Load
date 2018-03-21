using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileDamageOnWalk : Script_TileHandler {
	public int damages = 10;
	public Color onHitColor = Color.white;

	protected string environementName = "none";
	

	protected override void walkedOnEnter(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (entity) {
			entity.hit(damages, onHitColor, "environement", environementName);
		}
	}
	protected override void walkedOnStay(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (entity) {
			entity.hit(damages, onHitColor, "environement", environementName);
		}
	}	
}
