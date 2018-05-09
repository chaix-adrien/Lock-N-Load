using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_WeaponWithRay : Script_WeaponBase {
	public GameObject ray;
	new void Start () {
		weaponName = "raygun";
		base.Start();
	}

	void Update () {
		if (canFire || reloading)
			displayRay();
		ray.SetActive(canFire || reloading);
	}

	public override bool fire() {
		bool fired = base.fire();
		if (fired && ray) {
			ray.GetComponent<Script_Ray>().fire();
			if (getPercentAmmo() == 0f)
				ray.GetComponent<Script_Ray>().reload();
		}
		return fired;
	}

	protected override void onReloadStart() {
		if (ray)
			ray.GetComponent<Script_Ray>().reload();
	}

	protected override void onReloadEnd() {
		if (ray)
			ray.GetComponent<Script_Ray>().endReload();
	}

	void displayRay() {
		ray.SetActive(true);
		RaycastHit2D hitInfo = castRay();
		ray.GetComponent<LineRenderer>().SetPosition(0, startRay.transform.position);
		if (hitInfo) {
			ray.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
		}
	}
}
