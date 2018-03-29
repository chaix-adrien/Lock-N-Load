using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class TileData {
	private float rate;
	public ScriptedTile tile;
	public float percent;

	public TileData(float startRate) {rate = startRate;}
	public void set(float set) {rate = set;}
	public float get() {return rate;}
}

public class Script_GenerateTileRatePannel : MonoBehaviour {
	public GameObject tileTemplate;
	public GameObject pannel;
	public Script_CustomGame customGame;
	private List<ScriptedTile> tiles;
	private List<TileData> tileRates;
	private List<GameObject> generateds;
	private bool change = false;

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
				tmpRate.tile = tile;
				generated.GetComponentInChildren<Script_RateBlockListener>().tile = tmpRate;
				generated.SetActive(true);
				generateds.Add(generated);
				tileRates.Add(tmpRate);
				i += 1.0f;
			}
		}
		customGame.onBlockRate(tileRates);
	}
	
	public void valueChanged() {
		change = true;
	}

	private void generatePercent() {
		float chancesTotal;
		chancesTotal = 0f;
		foreach (TileData tile in tileRates) {
			chancesTotal += tile.get();
		}
		foreach (TileData tile in tileRates) {
			tile.percent = tile.get() / chancesTotal * 100;
		}
	}


	void Update() {
		if (change) {
			generatePercent();
			customGame.onBlockRate(tileRates);
			int i = 0;
			foreach (GameObject obj in generateds) {
				obj.GetComponentInChildren<Slider>().gameObject.GetComponentInChildren<Text>().text = tileRates[i].percent.ToString("0.0") + "%";
				i++;
			}
			change = false;
		}
	}
}
