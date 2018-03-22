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
public class Script_MapGenerator : MonoBehaviour {
	public bool generate = true;

	public bool clearAtGeneration = true;
	public Tile floor = null;

	public Tile wall = null;
	public avialableTile[] tiles;
	public Vector2Int size = new Vector2Int(19, 10);

	public GameObject[] playersToSpawn;

	private Tilemap tilemap;
	private List<float>chances;
	private float chancesTotal;

	private List<Vector3Int> floors;

	void Start() {
		tilemap = GetComponent<Tilemap>();
		if (generate)
			generateMap();
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

	private void spawnPlayer() {
		for (int i = 0; i < playersToSpawn.Length; i++) {
			playersToSpawn[i].transform.SetParent(transform);
			Vector3Int pos = floors[Mathf.FloorToInt(Random.Range(0, floors.Count))];
			playersToSpawn[i].transform.localPosition = new Vector3(pos.x + tilemap.tileAnchor.x, pos.y + tilemap.tileAnchor.y, pos.z + tilemap.tileAnchor.z);
			playersToSpawn[i].transform.Rotate(Vector3.forward, Random.Range(0, 360));
		}
	}

	private void generateMap() {
		if (!(wall & generate && tiles.Length > 0 && floor)) {
			Debug.Log("Check wall / floor / tiles");
			return;
		}
		if (clearAtGeneration)
			tilemap.ClearAllTiles();
		floors = new List<Vector3Int>();
		generateChances();
		generateBorders();
		Vector3Int pos = Vector3Int.zero;
		for (pos.x = 0; pos.x < size.x; pos.x++) {
			for (pos.y = 0; pos.y < size.y; pos.y++) {
				Tile chosenTile = choseRandomTile();
				tilemap.SetTile(pos, chosenTile);
				if (chosenTile == floor) {
					floors.Add(pos);
				}
			}
		}
		spawnPlayer();
		tilemap.CompressBounds();
		BoundsInt  bounds = tilemap.cellBounds;
	}
}
