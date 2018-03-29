using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CustomGame : MonoBehaviour {
	private int scoreToWin = 10;
	private int powerUpFrequency = 30;

	private List<TileData> tileToGenerate;
	public void onScrore(float s) {
		scoreToWin = Mathf.RoundToInt(s);
	}
	public void onPowerUp(float p) {
		powerUpFrequency = Mathf.RoundToInt(p);
	}

	public void onBlockRate(List<TileData> tileDatas) {
		tileToGenerate = tileDatas;
	}

}
