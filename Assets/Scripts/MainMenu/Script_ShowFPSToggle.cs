using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Script_ShowFPSToggle : MonoBehaviour {
	public Toggle  toggle;
	public GameObject menu;
	// Use this for initialization
	void Start () {
		toggle.isOn = menu.GetComponent<bl_PauseOptions>().ShowFramesPerSecond;
	}
	
}
