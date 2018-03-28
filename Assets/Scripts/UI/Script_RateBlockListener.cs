using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_RateBlockListener : MonoBehaviour {
	public TileData tile;
	// Use this for initialization
	void Start () {
		GetComponent<Slider>().onValueChanged.AddListener(delegate {valueChange();});
	}
	
	void valueChange() {
		tile.set(GetComponent<Slider>().value);
	}
}
