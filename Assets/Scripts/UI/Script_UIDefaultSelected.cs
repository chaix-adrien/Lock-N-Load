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

	public void selectDefault() {
		GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(selected);
	}
}
