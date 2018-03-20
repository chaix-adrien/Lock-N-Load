using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Script_TileChest : Script_Interactable {
	public GameObject[] contain;
	// Use this for initialization
	void Start () {
		base.Start();
	}
	public override void interactWith(GameObject interactor) {
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		Script_Tile_Collider tileCol = GetComponent<Script_Tile_Collider>();
		if (contain.Length > 0) {
			GameObject toReplace = contain[Mathf.FloorToInt(Random.value * contain.Length)];
		GameObject instancied = Instantiate(toReplace, new Vector3(tileCol.pos.x, tileCol.pos.y, 0), Quaternion.identity, tilemap.transform);
		instancied.transform.SetParent(tilemap.transform, false);		
		}
		Destroy(gameObject);
	}
}
