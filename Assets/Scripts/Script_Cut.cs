using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Cut : Script_WeaponBase {

	private SpriteRenderer rend;
	private IEnumerator coroutine;

	private bool state = false;
	// Use this for initialization
	void Start () {
		base.Start();
		rend = GetComponent<SpriteRenderer>();
	}
	
	protected override void shootOnShield(GameObject shield, Vector2 point) {
		
	}
	public override bool fire() {
		bool fired = base.fire();
		if (fired) {
			state = true;
			cutAnimation();
		}
		return fired;
	}

	public bool getState() {
		return state;
	}

	private void cutAnimation() {
		coroutine = cutAnimationCoroutine();
		StartCoroutine(coroutine);
	}
	private IEnumerator cutAnimationCoroutine() {
		float wait = 0.01f;
		float animationTime = 0.1f;
		float itTotal = animationTime / wait;
		float toAdd = 360 / itTotal;
		float rotate = 0;
		while (rotate < 360) {
			yield return new WaitForSeconds(wait);
			rotate += toAdd;
			transform.Rotate(Vector3.forward, toAdd);
		}
		state = false;
		StopCoroutine(coroutine);
	}
}
