﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void walkedOnEnter(Collider2D player) {

	}

	protected virtual void walkedOnStay(Collider2D player) {

	}

	protected virtual void walkedOnLeave(Collider2D player) {

	}

	public virtual void getShot(GameObject player) {
		Debug.Log("GetShot base");
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("walked on " + col);
		walkedOnEnter(col);
	}

	void OnTriggerExit2D(Collider2D col) {
		walkedOnLeave(col);
	}

	void OnTriggerStay2D(Collider2D col) {
		walkedOnStay(col);
	}

	
}
