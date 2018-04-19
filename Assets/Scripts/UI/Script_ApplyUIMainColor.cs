using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ApplyUIMainColor : MonoBehaviour {
	public bool UseMainColor = true;
	public Color color = Color.red;

	void Start() {
		applyToChildAndSeld(transform);
	}

	private void applyToChildAndSeld(Transform self) {
		foreach(Selectable sel in self.gameObject.GetComponents<Selectable>()) {
			ColorBlock cb = sel.colors;
			if (UseMainColor)
				cb.highlightedColor = Static_Datas.mainColor;
			else
				cb.highlightedColor = color;
			sel.colors = cb;
		}
		foreach (Transform child in self) {
			applyToChildAndSeld(child);
		}
	}
}
