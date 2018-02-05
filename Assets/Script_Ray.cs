﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ray : MonoBehaviour {
	public float fireTime;
	public Color StartChill;
	public Color EndChill;
	public Color StartFire;
	public Color EndFire;
	
	public GameObject player;
	private LineRenderer lineRenderer;
	private bool fireState = false;
	private bool reloadState = false;
	private float percentAmmo;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		percentAmmo = player.GetComponent<Script_Player>().getPercentAmmo();
		resetColor();
	}

	void Update() {
		percentAmmo = player.GetComponent<Script_Player>().getPercentAmmo();
		if (reloadState && !fireState) {
			lineRenderer.enabled = false;
		} else if (!reloadState) {
			lineRenderer.enabled = true;
		} if (fireState) {
			fireColor();
		} else {
			resetColor();
		}
	}

	public void fire() {
		fireState = true;		
		Invoke("endFire", fireTime);
	}

	void endFire() {
		fireState = false;
	}

	void fireColor() {
		lineRenderer.startColor = StartFire;
		lineRenderer.endColor = EndFire;
	}
	void resetColor() {
		Color newStart = StartChill;
		Color newEnd = EndChill;
		newStart.a = percentAmmo;
		newEnd.a = percentAmmo;
		lineRenderer.startColor = newStart;
		lineRenderer.endColor = newEnd;
	}
	public void reload() {
		reloadState = true;
	}

	public void endReload() {
		reloadState = false;
	}
}