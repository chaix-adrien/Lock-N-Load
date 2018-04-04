using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Presset = System.Collections.Generic.List<ToSaveTileData>;


public class TileData {
	private float rate;
	public ScriptedTile tile;
	public float percent;

	public TileData(float r, ScriptedTile t) {rate = r; tile = t; percent = 0;}
	public TileData() {rate = 0; tile = null; percent = 0;}
	public TileData(float startRate) {rate = startRate;}
	public void set(float set) {rate = set;}
	public float get() {return rate;}

	public static List<TileData> Combine(List<ScriptedTile> scripted, Presset presset) {
		List<TileData> ret = new List<TileData>();
		foreach (var pressetInfo in presset) {
			foreach (var tile in scripted) {
				if (pressetInfo.tileName == tile.name) {
					ret.Add(new TileData(pressetInfo.rate, tile));
					break;
				}
			}
		}
		return ret;
	}
}


[System.Serializable]
public class ToSaveTileData {
	public float rate;
	public string tileName;

	public ToSaveTileData(string name, float tileRate) {
		tileName = name;
		rate = tileRate;
	}

	public ToSaveTileData() {
		tileName = "none";
		rate = 0;
	}

	static public List<ToSaveTileData> Convert(List<TileData> toConvert) {
		List<ToSaveTileData> ret = new List<ToSaveTileData>();
		foreach (var data in toConvert) {
			ret.Add(new ToSaveTileData(data.tile.name, data.get()));
		}
		return ret;
	}
	static public ToSaveTileData Convert(TileData toConvert) {
		ToSaveTileData ret = new ToSaveTileData(toConvert.tile.name, toConvert.get());
		return ret;
	}

	public static void Apply(List<ToSaveTileData> from, List<TileData> toApply) {
		foreach (ToSaveTileData toSave in from) {
			foreach (TileData apply in toApply) {
				if (toSave.tileName == apply.tile.name) {
					apply.set(toSave.rate);
					break;
				}
			}
		}
	}
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
			if (tile.inMapGeneration)
				total += 1;
		}
		tiles.Sort(sortTilesByRate);
		foreach (ScriptedTile tile in tiles) {
			if (tile.inMapGeneration) {
				var generated = Instantiate(tileTemplate, Vector3.zero, Quaternion.identity);
				generated.transform.SetParent(pannel.transform, false);
				generated.transform.GetChild(0).GetComponentInChildren<Image>().sprite = tile.getDisplaySprite();
				generated.GetComponentInChildren<Text>().text = tile.tileName;
				generated.GetComponentInChildren<Slider>().value = tile.defaultRate;
				if (tile.floor)
					generated.GetComponentInChildren<Slider>().minValue = 1.0f;
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
	
	static int sortTilesByRate(ScriptedTile t1, ScriptedTile t2) {
		return t2.defaultRate.CompareTo(t1.defaultRate);
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

	public Presset getCurentPresset() {
		return ToSaveTileData.Convert(tileRates);
	}
	public void applyPresset(Presset presset) {
		ToSaveTileData.Apply(presset, tileRates);
	}

	void Update() {
		if (change) {
		foreach (TileData apply in tileRates) {
			apply.set(apply.get());
		}
		change = true;
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
