using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileExplodeCross : Script_TileExplosionBase {
	protected override void launchExplosion() {
		Vector2Int check = Vector2Int.zero;
		check = pos;
		for (check.x = pos.x - 1; (range == 0) ? true : check.x >= pos.x - range; check.x--) {
			if (!explode(check))
				break;
		}
		check = pos;
		for (check.x = pos.x + 1; (range == 0) ? true : check.x <= pos.x + range; check.x++) {
			if (!explode(check))
				break;
		}
		check = pos;
		for (check.y = pos.y - 1; (range == 0) ? true : check.y >= pos.y - range; check.y--) {
			if (!explode(check))
				break;
		}
		check = pos;
		for (check.y = pos.y + 1; (range == 0) ? true : check.y <= pos.y + range; check.y++) {
			if (!explode(check))
				break;
		}
		base.launchExplosion();
	}	
}
