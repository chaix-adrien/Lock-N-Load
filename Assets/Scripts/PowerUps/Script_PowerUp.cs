using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_PowerUp : Script_TileHandler {
	public bool onlyToPlayer = true;
	public bool destroyOnUse = true;
	public bool destroyOnShoot = true;
	public bool useIfUseless = false;
	private Script_PowerUpSpawner spawner;
	private Tilemap tilemap;
	private ScriptedTile floor;
	private Script_Tile_Collider colli;

	protected override void Start () {
		base.Start();
		tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		floor = Resources.Load("Floor_Tile") as ScriptedTile;
		colli = GetComponent<Script_Tile_Collider>();
		GameObject spawnerObj = GameObject.FindGameObjectWithTag("PowerUpSpawner");
		spawner = spawnerObj.GetComponent<Script_PowerUpSpawner>();
		spawner.addToSpawned(transform);
		if (!destroyOnShoot) {
			gameObject.layer = LayerMask.NameToLayer("IgnoredByRay");
		}
	}

	protected virtual bool use(Collider2D col) {
		return true;
	}

	protected virtual bool isUsefull(Collider2D col) {
		return false;
	}

	protected override void  walkedOnEnter(Collider2D col) {
		if (!col.isTrigger) {
			if ((onlyToPlayer && col.tag == "Player") || !onlyToPlayer) {
				if (useIfUseless || isUsefull(col)) {
					if (use(col) && destroyOnUse) {
						Destroy(gameObject);
					}
				}
			}
		}
	}

	public override void getShot(GameObject player, string from, string fromDetails) {
		if (destroyOnShoot) {
			Destroy(gameObject);
		}
	}
	
	void OnDestroy() {
		if (spawner)
			spawner.removeFromSpawned(transform);
	}
}
