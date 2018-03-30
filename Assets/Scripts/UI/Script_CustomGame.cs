using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Script_CustomGame : MonoBehaviour {
	private int scoreToWin = 10;
	private int powerUpFrequency = 30;

	private Vector2Int mapSize = new Vector2Int(19, 10);

	public GameObject mapPrerender;
	private GridLayoutGroup mapGrid;
	private Vector2 prerenderSize;
	private Dictionary<Sprite, float> tileWeight;
	private List<TileData> tileToGenerate;
	private bool changed = false;
	

	void Start() {
		tileWeight = new Dictionary<Sprite, float>();
		mapGrid = mapPrerender.GetComponent<GridLayoutGroup>();
		Vector3[] corners = new Vector3[4];
		prerenderSize = mapPrerender.GetComponent<RectTransform>().rect.size;
		prerenderSize.x -= (mapGrid.padding.left + mapGrid.padding.right);
		prerenderSize.y -= (mapGrid.padding.bottom + mapGrid.padding.top);
		refreshPrerenderMap();
		InvokeRepeating("refreshPrerenderMap", 4.0f, 4.0f);
		
	}
	public void onScrore(float s) {
		scoreToWin = Mathf.RoundToInt(s);
	}
	public void onPowerUp(float p) {
		powerUpFrequency = Mathf.RoundToInt(p);
	}

	public void onBlockRate(List<TileData> tileDatas) {
		tileToGenerate = tileDatas;
		if (tileWeight.Count == 0) {
			foreach (TileData data in tileDatas)
				tileWeight.Add(data.tile.sprite, data.percent);
		} else {
			foreach (TileData data in tileDatas)
				tileWeight[data.tile.sprite] = data.percent;
		}
		
		changed = true;
	}

	public void onHeight(float h) {
		mapSize.y = Mathf.RoundToInt(h);
		changed = true;
	}
	public void onWidth(float w) {
		mapSize.x = Mathf.RoundToInt(w);
		changed = true;
	}

	private void refreshPrerenderMap() {
		float size = 0f;
		if (prerenderSize.x / mapSize.x < prerenderSize.y / mapSize.y)
			size = prerenderSize.x / mapSize.x;
		else
			size = prerenderSize.y / mapSize.y;
		mapGrid.constraintCount = mapSize.x;
		mapGrid.cellSize = new Vector2(size, size);
		foreach (Transform child in mapPrerender.transform) {
     		GameObject.Destroy(child.gameObject);
 		}
		for (int i = 0; i < mapSize.x * mapSize.y; i++) {
			Sprite selected = WeightedRandomizer.From(tileWeight).TakeOne();
			GameObject NewObj = new GameObject(); //Create the GameObject
			Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
			NewImage.sprite = selected; //Set the Sprite of the Image Component on the new GameObject

			NewObj.GetComponent<RectTransform>().SetParent(mapPrerender.transform, false);
			NewObj.SetActive(true); //Activate the GameObject
		}


	}

	void Update() {
		if (changed) {
			refreshPrerenderMap();
			changed = false;
		}
	}

}
