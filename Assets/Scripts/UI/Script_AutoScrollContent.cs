using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class Script_AutoScrollContent : MonoBehaviour, IDeselectHandler, ISelectHandler {
	public Scrollbar scrollbar;
	public RectTransform rectToCheck;
	public RectTransform toBeWithin;
	public float scrollRatio;
	private bool selected = false;
	private int side = 0;

	void Update() {
		if (selected) {
			if (!rectToCheck.IsFullyVisibleFrom(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(), toBeWithin)) {
				if (side == 0)
					side = scrollRatio > scrollbar.value ? 1 : -1;
				scrollbar.value += 0.01f * side;
			} else
				side = 0;
		}
	}


	public void OnSelect(BaseEventData eventData)
     {
		 selected = true;
     }

     public void OnDeselect(BaseEventData eventData)
     {
		 selected = false;
     }
}
