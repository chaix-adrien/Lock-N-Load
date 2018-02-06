using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Mug : MonoBehaviour {

	public float speedCoef;
	// Use this for initialization
	private string contraintName;
	void Start () {
		contraintName = "mug_" + GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		Script_Move moveComp = col.gameObject.GetComponent<Script_Move>();
		if (moveComp) {
			moveComp.addContraint(contraintName, speedCoef);
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		Script_Move moveComp = col.gameObject.GetComponent<Script_Move>();
		if (moveComp) {
			moveComp.removeContraint(contraintName);
		}
	}
}
