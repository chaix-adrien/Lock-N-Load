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
	public ParticleSystem particle;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		percentAmmo = player.GetComponent<Script_WeaponWithRay>().getPercentAmmo();
		particle.Stop();
		particle.startColor = player.GetComponent<Script_Entity>().entityColor;
		resetColor();
	}

	void Update() {
		
		percentAmmo = player.GetComponent<Script_WeaponWithRay>().getPercentAmmo();
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
		var emitParams = new ParticleSystem.EmitParams();
		emitParams.rotation = -player.transform.eulerAngles.z - 180;
		particle.Emit(emitParams, 1);
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
		newStart.a = 0.5f + percentAmmo / 2f;
		newEnd.a = 0.5f + percentAmmo / 2f;
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
