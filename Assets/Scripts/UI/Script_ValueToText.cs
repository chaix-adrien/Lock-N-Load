using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ValueToText : MonoBehaviour {
	public Text text;	 
	private Slider valueObj;

	void Start() {
		valueObj = GetComponent<Slider>();
	}
	void Update () {
		if (valueObj.wholeNumbers)
			text.text = valueObj.value.ToString("0");
		else
			text.text = valueObj.value.ToString("0.0");
	}
}
