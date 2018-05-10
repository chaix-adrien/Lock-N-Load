using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileDestructible : Script_TileHandler {
	public Sprite[] spritesState;
	public ScriptedTile toPutWhenBroken;
	private int spriteId;
	private SpriteRenderer rend;

	protected override void Start () {
		base.Start();
		spriteId = 0;		
		rend = GetComponent<SpriteRenderer>();
	}

	public override void getShot(GameObject player, string from, string fromDetails) {
		base.getShot(player, from, fromDetails);
		if (spriteId < spritesState.Length) {
			rend.sprite = spritesState[spriteId];
			spriteId++;
		}
		if (spriteId >= spritesState.Length) {
			Invoke("destroySelf", 0.02f);
		}
	}
	private void destroySelf() {
		var tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		tilemap.SetTile(new Vector3Int(GetComponent<Script_Tile_Collider>().pos.x, GetComponent<Script_Tile_Collider>().pos.y, 0), toPutWhenBroken);
	}
}
