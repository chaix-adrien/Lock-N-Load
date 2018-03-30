using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_RateBlockListener : MonoBehaviour {
	public TileData tile;
	private bool fromUpdate = false;
	// Use this for initialization
	void Start () {
		GetComponent<Slider>().onValueChanged.AddListener(delegate {valueChange();});
	}
	
	void Update() {
		if (GetComponent<Slider>().value != tile.get()) {
			GetComponent<Slider>().value = tile.get();
			fromUpdate = true;
		}
	}

	void valueChange() {
		if (!fromUpdate) {
			tile.set(GetComponent<Slider>().value);
		}
		fromUpdate = false;
	}
}
