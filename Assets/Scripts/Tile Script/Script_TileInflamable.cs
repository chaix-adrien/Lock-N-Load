﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileInflamable : Script_TileHandler {

	public GameObject replacment;

	public override void getShot(GameObject player, string from, string fromDetails) {
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		Script_Tile_Collider tileCol= GetComponent<Script_Tile_Collider>();
		Instantiate(replacment, new Vector3(tileCol.pos.x, tileCol.pos.y, 0), Quaternion.identity, tilemap.transform);
		Destroy(gameObject);
	}
}
