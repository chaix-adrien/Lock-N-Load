using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ResetTiles : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		foreach (Script_Tile_Collider tile in GameObject.FindObjectsOfTypeAll(typeof(Script_Tile_Collider))) {
			tile.resetSprite();
		}
	}
}
