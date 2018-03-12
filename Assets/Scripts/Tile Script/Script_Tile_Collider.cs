﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_Tile_Collider : MonoBehaviour {

	public bool crossableByRay = true;
	public ScriptedTile tile;
	private Sprite savedSprite;
	void Start () {
		Vector3 pos = transform.localPosition;
		transform.localPosition = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
		if (crossableByRay) {
			gameObject.layer = LayerMask.NameToLayer("IgnoredByRay");
		}
	}
	public void setSprite(Sprite toset) {
		tile.sprite = toset;
	}

    void OnDestroy() {
				tile.resetSprite();
    }
}