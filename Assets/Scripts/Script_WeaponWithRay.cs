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
		if (canFire)
			displayRay();
		ray.SetActive(canFire);
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


	protected override void onReloadEnd() {
		Debug.Log("3");
		if (ray)
			ray.GetComponent<Script_Ray>().endReload();
	}

	void displayRay() {
		RaycastHit2D hitInfo = castRay();
		ray.GetComponent<LineRenderer>().SetPosition(0, startRay.transform.position);
		if (hitInfo) {
			ray.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
		}
	}
}
