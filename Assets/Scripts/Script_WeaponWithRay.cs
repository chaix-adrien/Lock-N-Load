using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_WeaponWithRay : MonoBehaviour {
	public float reloadTime = 2f;
	public int magazineMax = 3;
	public float speedFire = 0.0f;
	public int damagePerShot = 20;
	public GameObject impact;
	public Transform startRay;
	public GameObject ray;
	private int magazine;
	private float lastShootTime = 0f;

	private bool canFire = true;
	private Dictionary<string, bool> contraints;

	// Use this for initialization
	void Start () {
		magazine = magazineMax;
		contraints = new Dictionary<string, bool>();
	}
	
	// Update is called once per frame
	void Update () {
		if (canFire)
			displayRay();
	}

	public void fire() {
		if (canFire
		&& lastShootTime + speedFire <= Time.time
		&& magazine > 0) {
			if (ray)
				ray.GetComponent<Script_Ray>().fire();
			RaycastHit2D hitInfo = castRay();
			Script_TileHandler tileHandler = hitInfo.collider.gameObject.GetComponent<Script_TileHandler>();
			if (tileHandler)
				tileHandler.getShot(gameObject);
			Script_Entity entityHandler = hitInfo.collider.gameObject.GetComponent<Script_Entity>();
			if (entityHandler)
				entityHandler.hit(damagePerShot, GetComponent<Script_Entity>().entityColor);
			GameObject instanciedImpact = Instantiate(impact, hitInfo.point, Quaternion.identity);
			instanciedImpact.GetComponent<SpriteRenderer>().color = GetComponent<Script_Entity>().entityColor;
			magazine--;
			lastShootTime = Time.time;
			if (magazine <= 0)
				Invoke("reload", reloadTime);				
		}
	}

	public void reload() {
		magazine = magazineMax;
		if (ray)
			ray.GetComponent<Script_Ray>().endReload();
	}

	public float getPercentAmmo() {
		return (magazine / 1f) / (magazineMax / 1f);
	}

	public void addContraint(string name, bool enableWeapon) {
		if (contraints.ContainsKey(name)) {
			contraints[name] = enableWeapon;
		} else {
			contraints.Add(name, enableWeapon);
		}
		updateCanFire();
	}

	public void removeContraint(string name) {
		contraints.Remove(name);
		updateCanFire();
	}

	void updateCanFire() {
		bool can = true;
		foreach(KeyValuePair<string, bool> contraint in contraints) {
			can = can && contraint.Value;
		}
		canFire = can;
		ray.SetActive(canFire);
	}

	void displayRay() {
		RaycastHit2D hitInfo = castRay();
		ray.GetComponent<LineRenderer>().SetPosition(0, startRay.transform.position);
		if (hitInfo) {
			ray.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
		}
	}
	RaycastHit2D castRay() {
		string[] mask = {"Default", "Player"};
		int layerMask = LayerMask.GetMask(mask);
		return Physics2D.Raycast(startRay.transform.position, transform.up, Mathf.Infinity, layerMask);
	}
}
