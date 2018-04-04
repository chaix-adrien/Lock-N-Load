using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Script_LoadTiles))]
public class Script_ResetTiles : MonoBehaviour {
	List<ScriptedTile> tiles;
	// Use this for initialization
	void Start () {
		tiles = GetComponent<Script_LoadTiles>().getLoadedTiles();
		foreach (ScriptedTile tile in tiles) {
			tile.setInGameSprite();
		}
	}

	void OnDestroy() {
		foreach (ScriptedTile tile in tiles) {
			tile.resetSprite();
		}
	}
}
