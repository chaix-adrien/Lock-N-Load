using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Script_PowerUpSpawner : MonoBehaviour {
	public float spawnRate = 2f;
	public float timeBeforeFirst = 1f;

	public GameObject[] toSpawn;

	private GameObject map;
	private Tilemap tilemap;

	private List<Transform> spawned;
	private List<Vector3> spawnedPos;

	// Use this for initialization
	void Start () {
		map =  GameObject.FindGameObjectWithTag("Map");
		tilemap = map.GetComponent<Tilemap>();
		startSpawn(true);
		spawned = new List<Transform>();
		spawnedPos = new List<Vector3>();
	}
	
	public void startSpawn(bool beforeTime) {
		InvokeRepeating("spawn", beforeTime ? timeBeforeFirst : 0f, spawnRate);
	}

	public void stopSpawn() {
		CancelInvoke("spawn");
	}

	public void addToSpawned(Transform powerup) {
		spawned.Add(powerup);
		spawnedPos.Add(powerup.localPosition);
	}
	
	public void removeFromSpawned(Transform powerup) {
		spawned.Remove(powerup);
		spawnedPos.Remove(powerup.localPosition);
	}

	private void spawn() {
		List<Vector3> avialableTiles = new List<Vector3>();
		for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; x++) {
            for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; y++) {
				Tile tile = tilemap.GetTile(new Vector3Int(x, y, 0)) as Tile;
				ScriptedTile scriptedTile = tile as ScriptedTile;
				if (tile) {
					if ((scriptedTile && scriptedTile.allowPowerUp) || (!scriptedTile && tile.colliderType != Tile.ColliderType.Sprite)) {
						if (spawnedPos.Contains(new Vector3(x + 0.5f, y + 0.5f, 0)) == false) {
							avialableTiles.Add(new Vector3(x, y, 0));
						}
					}
				}
            }
        }
		if (avialableTiles.Count != 0) {
			Vector3 selectedTilePos = avialableTiles[Mathf.FloorToInt(Random.value * avialableTiles.Count)];
			GameObject toCreate = toSpawn[Mathf.FloorToInt(Random.value * toSpawn.Length)];
			GameObject created = Instantiate(toCreate, selectedTilePos, Quaternion.identity);
			created.transform.SetParent(map.transform, false);
		}
	}
}
