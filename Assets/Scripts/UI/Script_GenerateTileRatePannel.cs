using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Script_GenerateTileRatePannel : MonoBehaviour {
	public GameObject tileTemplate;
	public GameObject pannel;
	private List<ScriptedTile> tiles;
	private List<GameObject> generateds;

	void Start () {
		tiles = GetComponent<Script_LoadTiles>().getLoadedTiles();
		generateds = new List<GameObject>();
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
				generated.GetComponentInChildren<Script_RateBlockListener>().tile = tile;
				generated.SetActive(true);
				generateds.Add(generated);
				i += 1.0f;
			}
		}
	}

	private List<float>chances;
	private List<int>percent;


	private void generatePercent() {
		float chancesTotal;
		percent = new List<int>();
		chancesTotal = 0f;
		foreach (ScriptedTile tile in tiles) {
			if (!tile.floor)
				chancesTotal += (tile.defaultRate > 1f) ? tile.defaultRate : 1f;
		}
		foreach (ScriptedTile tile in tiles) {
			if (!tile.floor) {
				percent.Add(Mathf.RoundToInt(tile.defaultRate / chancesTotal * 100));
			}
		}
	}


	void Update() {
		generatePercent();
		int i = 0;
		foreach (GameObject obj in generateds) {
			obj.GetComponentInChildren<Slider>().gameObject.GetComponentInChildren<Text>().text = percent[i] + "%";
			i++;
		}
	}
}
