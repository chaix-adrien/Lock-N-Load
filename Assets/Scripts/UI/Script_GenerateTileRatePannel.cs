using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Script_GenerateTileRatePannel : MonoBehaviour {
	public GameObject tileTemplate;
	public GameObject pannel;
	private List<ScriptedTile> tiles;
	void Start () {
		tiles = GetComponent<Script_LoadTiles>().getLoadedTiles();
		float i = 0;
		float total = 0;
		foreach (ScriptedTile tile in tiles) {
			if (tile.floor == false)
				total += 1;
		}
		foreach (ScriptedTile tile in tiles) {
			if (tile.floor == false) {
				var generated = Instantiate(tileTemplate, Vector3.zero, Quaternion.identity);
				generated.transform.SetParent(pannel.transform, false);
				generated.transform.GetChild(0).GetComponentInChildren<Image>().sprite = tile.getDisplaySprite();
				generated.GetComponentInChildren<Text>().text = tile.tileName;
				generated.GetComponentInChildren<Slider>().value = tile.defaultRate;
				generated.GetComponentInChildren<Script_AutoScrollContent>().scrollRatio = 1 - i / (total - 1.0f);
				generated.SetActive(true);
				i += 1.0f;
			}
		}
	}
}
