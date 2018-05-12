using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Presset = System.Collections.Generic.List<ToSaveTileData>;
using XboxCtrlrInput;

[System.Serializable]
public class Script_CustomGame : MonoBehaviour {
	public GameObject mapPrerender;
	public Selectable onStart;
	public Selectable onB;
	public GameObject warningPannel;
	public Selectable toSelectOnLeave;

	private GridLayoutGroup mapGrid;
	private Vector2 prerenderSize;
	private Dictionary<Sprite, float> tileWeight;
	private List<TileData> tileToGenerate;
	private Presset toSaveTileData;
	private bool changed = false;	

	void Start() {
		tileWeight = new Dictionary<Sprite, float>();
		mapGrid = mapPrerender.GetComponent<GridLayoutGroup>();
		prerenderSize = mapPrerender.GetComponent<RectTransform>().rect.size;
		prerenderSize.x -= (mapGrid.padding.left + mapGrid.padding.right);
		prerenderSize.y -= (mapGrid.padding.bottom + mapGrid.padding.top);
		refreshPrerenderMap();
		mapPrerender.GetComponent<ContentSizeFitter>().enabled = true;
		InvokeRepeating("refreshPrerenderMap", 4.0f, 4.0f);
	}

	void Update() {
		if (changed) {
			refreshPrerenderMap();
			changed = false;
		}
		//Debug.Log(XCI.GetButton(XboxButton.Start, XboxController.Second));
		if (XCI.GetButtonDown(XboxButton.Start))
			onStart.Select();
		if (XCI.GetButtonDown(XboxButton.B, XboxController.Any))
			onB.Select();
	}

	public void onScrore(float s) {
		Static_Datas.scoreToWin = Mathf.RoundToInt(s);
	}
	public void onPowerUp(float p) {
		Static_Datas.poerUpSpawnTime = Mathf.RoundToInt(p);
	}

	public void onBack() {
		gameObject.SetActive(false);
		toSelectOnLeave.Select();
	}

	public void onBlockRate(List<TileData> tileDatas) {
		tileToGenerate = tileDatas;
		if (tileWeight.Count == 0) {
			foreach (TileData data in tileDatas)
				tileWeight.Add(data.tile.getDisplaySprite(), data.percent);
		} else {
			foreach (TileData data in tileDatas)
				tileWeight[data.tile.getDisplaySprite()] = data.percent;
		}
		
		changed = true;
	}

	public void onHeight(float h) {
		Static_Datas.sizeMap.y = Mathf.RoundToInt(h);
		changed = true;
	}
	public void onWidth(float w) {
		Static_Datas.sizeMap.x = Mathf.RoundToInt(w);
		changed = true;
	}

	public void onGenerate() {
#if UNITY_EDITOR
#else
		if (XCI.GetNumPluggedCtrlrs() < 2) {
			warningPannel.SetActive(true);
			warningPannel.transform.GetChild(0).gameObject.SetActive(true);
			return;
		}
#endif
		Static_Datas.presset = ToSaveTileData.Convert(tileToGenerate);
		SceneManager.LoadScene("Game");
	}

	private void refreshPrerenderMap() {
		float size = 0f;
		if (prerenderSize.x / Static_Datas.sizeMap.x < prerenderSize.y / Static_Datas.sizeMap.y)
			size = prerenderSize.x / Static_Datas.sizeMap.x;
		else
			size = prerenderSize.y / Static_Datas.sizeMap.y;
		mapGrid.constraintCount = Static_Datas.sizeMap.x;
		mapGrid.cellSize = new Vector2(size, size);
		foreach (Transform child in mapPrerender.transform) {
     		GameObject.Destroy(child.gameObject);
 		}
		for (int i = 0; i < Static_Datas.sizeMap.x * Static_Datas.sizeMap.y; i++) {
			Sprite selected = WeightedRandomizer.From(tileWeight).TakeOne();
			GameObject NewObj = new GameObject();
			Image NewImage = NewObj.AddComponent<Image>();
			NewImage.sprite = selected;

			NewObj.GetComponent<RectTransform>().SetParent(mapPrerender.transform, false);
			NewObj.SetActive(true);
		}
	}
}
