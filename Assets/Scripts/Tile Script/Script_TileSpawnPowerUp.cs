using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileSpawnPowerUp : MonoBehaviour {
	public GameObject toSpawn = null;
	// Use this for initialization
	void Start () {
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		if (tilemap) {
			Tile floor = Resources.Load("tiles Assets/Floor_Tile") as Tile;
			Script_Tile_Collider tileCol = GetComponent<Script_Tile_Collider>();
			if (toSpawn) {
				GameObject instancied = Instantiate(toSpawn, new Vector3(tileCol.pos.x, tileCol.pos.y, 0), Quaternion.identity);
				instancied.transform.SetParent(tilemap.transform, false);
				tilemap.SetTile(new Vector3Int(tileCol.pos.x, tileCol.pos.y, 0), floor);
			}
		}
	}
}
