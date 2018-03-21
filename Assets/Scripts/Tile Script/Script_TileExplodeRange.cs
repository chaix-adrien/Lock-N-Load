﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileExplodeRange : Script_TileExplosionBase {
	protected override void launchExplosion() {
		Vector2Int check = Vector2Int.zero;
		for (check.x = pos.x - range; check.x <= pos.x + range; check.x++) {
			for (check.y = pos.y - range; check.y <= pos.y + range; check.y++) {
				explode(check);
			}	
		}
	}
}
