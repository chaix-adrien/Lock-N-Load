using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_GiveHighlightToAllSelectable : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Selectable[] childs = GetComponentsInChildren<Selectable>();
		foreach (Selectable child in childs) {
			child.gameObject.AddComponent<HighlightFix>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
