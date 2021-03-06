﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Script_Tile_Collider : MonoBehaviour {
	public bool crossableByRay = true;
	public bool walkable = false;
	public ScriptedTile tile;
	public Vector2Int pos;
	private Sprite savedSprite;
	void Start () {
		init(SceneManager.GetActiveScene(), LoadSceneMode.Single);
	}

	private void init(Scene scene, LoadSceneMode mode) {
		if (scene == SceneManager.GetSceneByName("MainMenu")) {
			gameObject.SetActive(false);
			return;
		}
		gameObject.SetActive(true);
		pos.x = Mathf.FloorToInt(transform.localPosition.x);
		pos.y = Mathf.FloorToInt(transform.localPosition.y);
		transform.localPosition = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
		if (crossableByRay) {
			gameObject.layer = LayerMask.NameToLayer("IgnoredByRay");
		}
		Collider2D col = GetComponent<Collider2D>();
		if (col)
			col.isTrigger = walkable;
	}
}
