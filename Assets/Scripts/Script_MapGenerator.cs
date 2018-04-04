using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Presset = System.Collections.Generic.List<ToSaveTileData>;


public class Script_MapGenerator : MonoBehaviour {
	public bool clearAtGeneration = true;

	protected Presset presset;
	public List<GameObject> playersToSpawn;
	protected Tilemap tilemap;
	protected List<ScriptedTile> avialableTiles;
	protected List<Vector3Int> floors;

	protected virtual void Start() {
		tilemap = GetComponent<Tilemap>();
		avialableTiles = GetComponent<Script_LoadTiles>().getLoadedTiles();
	}

	protected void spawnPlayer() {
		for (int i = 0; i < playersToSpawn.Count; i++) {
			playersToSpawn[i].transform.SetParent(transform);
			Vector3Int pos = floors[Mathf.FloorToInt(Random.Range(0, floors.Count))];
			playersToSpawn[i].transform.localPosition = new Vector3(pos.x + tilemap.tileAnchor.x, pos.y + tilemap.tileAnchor.y, pos.z + tilemap.tileAnchor.z);
			playersToSpawn[i].transform.Rotate(Vector3.forward, Random.Range(0, 360));
		}
	}

	public void setPresset(Presset pres) {
		presset = pres;
	}

	protected void getFloorsTiles() {
		BoundsInt bounds = tilemap.cellBounds;
		Vector3Int pos = Vector3Int.zero;
		floors = new List<Vector3Int>();
		for (pos.x = bounds.position.x; pos.x < bounds.size.x; pos.x++) {
			for (pos.y = bounds.position.y; pos.y < bounds.size.y; pos.y++) {
				ScriptedTile tile = tilemap.GetTile(pos) as ScriptedTile;;
				if (tile && tile.floor)
					floors.Add(pos);
			}
		}
	}

	public virtual void generateMap() {
		tilemap.CompressBounds();
		getFloorsTiles();
		spawnPlayer();
	}
}
