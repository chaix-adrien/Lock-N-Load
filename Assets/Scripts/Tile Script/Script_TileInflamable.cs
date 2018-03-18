using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileInflamable : Script_TileHandler {

	public GameObject replacment;

	public override void getShot(GameObject player) {
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		GameObject explosion = Instantiate(replacment, new Vector3(pos.x, pos.y, 0), Quaternion.identity, tilemap.transform);
		Destroy(gameObject);
	}
}
