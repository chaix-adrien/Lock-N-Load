using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_LifeBar : MonoBehaviour {
	public bool left = false;
	private float max;


	void Start() {
		max = GetComponent<LineRenderer>().GetPosition(1).x;
	}

	public void refreshLifeBar(float percent) {
		var pos = GetComponent<LineRenderer>().GetPosition(1);
		pos.x = max * (left ? percent : 1 - percent);
		GetComponent<LineRenderer>().SetPosition(left ? 1 : 0, pos);
		//transform.localScale = new Vector3(percent, 1f, 1f);
	}
}
