using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Script_TileChest : Script_Interactable {
	public GameObject[] contain;
	public AudioClip onOpenSound;
	private Tile floor;
	protected override void Start () {
		base.Start();
		floor = Resources.Load("tiles Assets/Floor_Tile") as Tile;
	}
	public override void interactWith(GameObject interactor) {
		Tilemap tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		Script_Tile_Collider tileCol = GetComponent<Script_Tile_Collider>();
		if (contain.Length > 0) {
			GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onOpenSound);
			GameObject toReplace = contain[Mathf.FloorToInt(Random.value * contain.Length)];
			if (toReplace) {
				GameObject instancied = Instantiate(toReplace, new Vector3(tileCol.pos.x, tileCol.pos.y, 0), Quaternion.identity);
				instancied.transform.SetParent(tilemap.transform, false);
				tilemap.SetTile(new Vector3Int(tileCol.pos.x, tileCol.pos.y, 0), floor);				
			}
		}
	}
}
