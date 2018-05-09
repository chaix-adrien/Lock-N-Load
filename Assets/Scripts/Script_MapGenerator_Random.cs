using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Presset = System.Collections.Generic.List<ToSaveTileData>;

public class Script_MapGenerator_Random : Script_MapGenerator {
	public Tile floor = null;
	public Tile wall = null;
	private Vector2Int size = new Vector2Int(19, 10);
	private List<TileData> tileDatas;
	private Dictionary<ScriptedTile, float> tileWeight;
	private List<float>chances;
	private float chancesTotal;
	
	protected override void Start() {
		base.Start();
		size = Static_Datas.sizeMap;
		if (Static_Datas.presset == null) {
			Static_Datas.presset = GetComponent<Script_PersistentLoader>().getSave().GetValue<Presset>("Default");			
		}
	}

	private void generateChances() {
		tileWeight = new Dictionary<ScriptedTile, float>();
		foreach (ToSaveTileData data in presset) {
			foreach(ScriptedTile scripted in avialableTiles) {
				if (data.tileName == scripted.name) {
					tileWeight.Add(scripted, data.rate);
					break;
				}
			}
		}
			
	}
	private void generateBorders() {
		Vector3Int pos = Vector3Int.zero;
		for (pos.x = -2; pos.x <= size.x + 1; pos.x++) {
			for (pos.y = -2; pos.y <= size.y + 1; pos.y++) {
				if (pos.x < 0 || pos.x >= size.x || pos.y < 0 || pos.y >= size.y) {
					tilemap.SetTile(pos, wall);
				}
			}
		}
	}

	
	public override void generateMap() {
		Debug.Assert(wall && avialableTiles.Count > 0 && floor && tilemap);
		if (clearAtGeneration) {
			tilemap.ClearAllTiles();
			 foreach (Transform child in transform) {
     			if (child.tag != "Player")
				 GameObject.Destroy(child.gameObject);
 			}
		}
			
		floors = new List<Vector3Int>();
		generateChances();
		generateBorders();
		Vector3Int pos = Vector3Int.zero;
		for (pos.x = 0; pos.x < size.x; pos.x++) {
			for (pos.y = 0; pos.y < size.y; pos.y++) {
				ScriptedTile chosenTile = WeightedRandomizer.From(tileWeight).TakeOne();
				tilemap.SetTile(pos, chosenTile);
			}
		}
		base.generateMap();
	}
}
