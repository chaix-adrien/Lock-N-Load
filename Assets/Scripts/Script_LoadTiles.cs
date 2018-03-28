using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

 public class Script_LoadTiles : MonoBehaviour {

	public string folder = "tiles Assets";
	private List<ScriptedTile> loadedTiles;
	void Start () {
		reloadTiles();
	}

	public List<ScriptedTile> getLoadedTiles() {
		return loadedTiles;
	}
	public bool reloadTiles(string path = null) {
		if (path == null)
			path = folder;
		loadedTiles = new List<ScriptedTile>();
		Debug.Log(Resources.LoadAll(path).Length);
		AssetBundle bundle = new AssetBundle();
		foreach (ScriptedTile tile in Resources.LoadAll(path)) {
			loadedTiles.Add(tile);
		}
		return loadedTiles.Count == 0;
	}
}
