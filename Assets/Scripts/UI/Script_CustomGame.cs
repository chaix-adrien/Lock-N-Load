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
	private List<TileData> tileToGenerate;
	private bool changed = false;
	

	void Start() {
		mapGrid = mapPrerender.GetComponent<GridLayoutGroup>();
		Vector3[] corners = new Vector3[4];
		prerenderSize = mapPrerender.GetComponent<RectTransform>().rect.size;
		prerenderSize.x -= (mapGrid.padding.left + mapGrid.padding.right);
		prerenderSize.y -= (mapGrid.padding.bottom + mapGrid.padding.top);
	}
	public void onScrore(float s) {
		scoreToWin = Mathf.RoundToInt(s);
	}
	public void onPowerUp(float p) {
		powerUpFrequency = Mathf.RoundToInt(p);
	}

	public void onBlockRate(List<TileData> tileDatas) {
		tileToGenerate = tileDatas;
		//changed = true;
	}

	public void onHeight(float h) {
		mapSize.y = Mathf.RoundToInt(h);
		changed = true;
	}
	public void onWidth(float w) {
		mapSize.x = Mathf.RoundToInt(w);
		changed = true;
	}

	void Update() {
		if (changed) {
			float size = 0f;
			Debug.Log(prerenderSize);
			Debug.Log(prerenderSize.x / mapSize.x + " / " + prerenderSize.y / mapSize.y);
			if (prerenderSize.x / mapSize.x < prerenderSize.y / mapSize.y)
				size = prerenderSize.x / mapSize.x;
			else
				size = prerenderSize.y / mapSize.y;
			mapGrid.constraintCount = mapSize.x;
			mapGrid.cellSize = new Vector2(size, size);
			changed = false;
		}
	}

}
