using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileExplodeAround : Script_TileExplosionBase {
	protected override void launchExplosion(GameObject madeExplosion) {
		Vector2Int check = Vector2Int.zero;
		for (check.x = pos.x - range; check.x <= pos.x + range; check.x++) {
			for (check.y = pos.y - range; check.y <= pos.y + range; check.y++) {
				if (!(check.x == pos.x && check.y == pos.y)) {
					explode(check, madeExplosion);
				}
			}	
		}
		base.launchExplosion(madeExplosion);
	}
}
