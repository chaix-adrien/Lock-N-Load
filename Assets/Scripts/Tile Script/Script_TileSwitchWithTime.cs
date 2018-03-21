using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileSwitchWithTime : MonoBehaviour {
	public float switchTime = 1f;
	public Tile toSwitch = null;
	void Start () {
		Invoke("switchTile", switchTime);
	}
	
	void switchTile() {
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		Vector2Int pos = GetComponent<Script_Tile_Collider>().pos;
		tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), toSwitch);
	}
}
