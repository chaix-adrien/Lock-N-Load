using System.Collections;
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
		percentAmmo = player.GetComponent<Script_WeaponWithRay>().getPercentAmmo();
		resetColor();
	}

	void OnEnable() {
		if (!lineRenderer)
			return;
		if (!fireState && reloadState) {
			lineRenderer.enabled = false;
		} else {
			lineRenderer.enabled = true;
		}
	}

	void Update() {
		percentAmmo = player.GetComponent<Script_WeaponWithRay>().getPercentAmmo();
		if (!fireState && reloadState) {
			lineRenderer.enabled = false;
		} else {
			lineRenderer.enabled = true;
		}
		if (fireState) {
			lineRenderer.endWidth += 0.06f;
			fireColor();
		} else {
			if (lineRenderer.endWidth > 0.2f) {
				lineRenderer.endWidth -= 0.1f;
				lineRenderer.endWidth = lineRenderer.endWidth < 0.2f ? 0.2f : lineRenderer.endWidth;
			}
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
		newStart.a = percentAmmo > 0f ? 0.5f + percentAmmo / 2f : 0f;
		newEnd.a = percentAmmo > 0f ? 0.5f + percentAmmo / 2f : 0f;
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
