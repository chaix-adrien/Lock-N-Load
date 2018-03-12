using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_LifeBar : MonoBehaviour {

	private Script_Entity entity;
	private LineRenderer line;
	// Use this for initialization
	void Start () {
		entity = GetComponent<Script_Entity>();
		line = GetComponent<LineRenderer>();
	}
	
	public void refreshLifeBar(float percent) {
		transform.localScale = new Vector3(percent, 1f, 1f);
	}
}
