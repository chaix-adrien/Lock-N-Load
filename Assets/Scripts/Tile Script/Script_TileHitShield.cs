using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XboxCtrlrInput;

public class Script_TileHitShield : Script_TileHandler {

	public bool downEntierely;
	public int hitCharge;

	public Tile replaceByOnWalk = null;
	
	protected override void walkedOnEnter(Collider2D col) {
		Script_Shield shield = col.GetComponentInChildren<Script_Shield>();
		if (shield) {
			Script_Player player = col.gameObject.GetComponentInChildren<Script_Player>();
			if (player)
				player.gamepad.SetVibration(0.1f, 0.5f);
			if (downEntierely)
				shield.hit(-1);
			else
				shield.hit(hitCharge);
			if (replaceByOnWalk) {
				Invoke("destroySelf", 0.001f);
			}
		}
	}

	private void destroySelf() {
		Vector2Int pos = GetComponent<Script_Tile_Collider>().pos;
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), replaceByOnWalk);
	}
}
