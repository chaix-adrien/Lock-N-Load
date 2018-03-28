using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class TileData {
	private float rate;

	public TileData(float startRate) {rate = startRate;}
	public void set(float set) {rate = set;}
	public float get() {return rate;}
}

public class Script_GenerateTileRatePannel : MonoBehaviour {
	public GameObject tileTemplate;
	public GameObject pannel;
	private List<ScriptedTile> tiles;
	private List<TileData> tileRates;
	private List<GameObject> generateds;

	void Start () {
		tiles = GetComponent<Script_LoadTiles>().getLoadedTiles();
		generateds = new List<GameObject>();
		tileRates = new List<TileData>();
		float i = 0;
		float total = 0;
		foreach (ScriptedTile tile in tiles) {
			if (tile.floor == false)
				total += 1;
		}
		foreach (ScriptedTile tile in tiles) {
			if (tile.inMapGeneration) {
				var generated = Instantiate(tileTemplate, Vector3.zero, Quaternion.identity);
				generated.transform.SetParent(pannel.transform, false);
				generated.transform.GetChild(0).GetComponentInChildren<Image>().sprite = tile.getDisplaySprite();
				generated.GetComponentInChildren<Text>().text = tile.tileName;
				generated.GetComponentInChildren<Slider>().value = tile.defaultRate;
				generated.GetComponentInChildren<Script_AutoScrollContent>().scrollRatio = 1 - i / (total - 1.0f);
				TileData tmpRate = new TileData(tile.defaultRate);
				generated.GetComponentInChildren<Script_RateBlockListener>().tile = tmpRate;
				generated.SetActive(true);
				generateds.Add(generated);
				tileRates.Add(tmpRate);
				i += 1.0f;
			}
		}
	}
	


	private List<float> generatePercent() {
		float chancesTotal;
		List<float> percent = new List<float>();
		chancesTotal = 0f;
		foreach (TileData tile in tileRates) {
			chancesTotal += (tile.get() > 1f) ? tile.get() : 1f;
		}
		foreach (TileData tile in tileRates) {
			percent.Add(tile.get() / chancesTotal * 100);
		}
		return percent;
	}


	void Update() {
		List<float> percent = generatePercent();
		int i = 0;
		foreach (GameObject obj in generateds) {
			obj.GetComponentInChildren<Slider>().gameObject.GetComponentInChildren<Text>().text = percent[i].ToString("0.0") + "%";
			i++;
		}
	}
}
