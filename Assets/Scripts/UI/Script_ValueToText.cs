using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ValueToText : MonoBehaviour {
	public string prefix = "";
	public string suffix = "";
	public Text text;
	private Slider valueObj;

	void Start() {
		valueObj = GetComponent<Slider>();
	}
	void Update () {
		if (valueObj.wholeNumbers)
			text.text = prefix + valueObj.value.ToString("0") + suffix;
		else
			text.text = prefix + valueObj.value.ToString("0.0") + suffix;
	}
}
