using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Script_UIDefaultSelected : MonoBehaviour {

	public GameObject selected;
	// Use this for initialization
	void Start () {
		selectDefault();
	}
	void OnEnable() {
		selectDefault();
	}

	public void selectDefault() {
		selected.GetComponent<Selectable>().Select();
		selected.GetComponent<Selectable>().OnSelect(null);
		EventSystem.current.SetSelectedGameObject(selected);
	}
}
