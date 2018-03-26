using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
 public struct avialableTile
 {
     [SerializeField] public Tile tile;
	 [SerializeField] public float chance;
 }
public class Script_MapGenerator_Random : Script_MapGenerator {
	public Tile floor = null;
	public Tile wall = null;
	public avialableTile[] tiles;
	private Vector2Int size = new Vector2Int(19, 10);

	private List<float>chances;
	private float chancesTotal;
	
	protected override void Start() {
		base.Start();
		size = Static_Datas.sizeMap;
	}

	private void generateChances() {
		chances = new List<float>();
		chancesTotal = 0f;
		float total = 0f;
		for (int i = 0; i < tiles.Length; i++) {
			chancesTotal += (tiles[i].chance > 1f) ? tiles[i].chance : 1f;
			total += tiles[i].chance;
			chances.Add(total);
		}
	}

	private Tile choseRandomTile() {
		float chosenRange = Random.Range(0f, chancesTotal);
		for (int i = 0; i < tiles.Length; i++) {
			if (chances[i] > chosenRange)
				return tiles[i].tile;
		}
		return floor;
	}

	private void generateBorders() {
		Vector3Int pos = Vector3Int.zero;
		for (pos.x = -1; pos.x <= size.x; pos.x++) {
			for (pos.y = -1; pos.y <= size.y; pos.y++) {
				if (pos.x == -1 || pos.x == size.x || pos.y == -1 || pos.y == size.y) {
					tilemap.SetTile(pos, wall);
				}
			}
		}
	}

	
	public override void generateMap() {
		Debug.Assert(wall && tiles.Length > 0 && floor && tilemap);
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
				Tile chosenTile = choseRandomTile();
				tilemap.SetTile(pos, chosenTile);
			}
		}
		base.generateMap();
	}
}
