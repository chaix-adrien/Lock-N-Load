using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_WeaponBase : MonoBehaviour {
	public float reloadTime = 2f;
	public int magazineMax = 3;
	public float speedFire = 0.0f;
	public float range = Mathf.Infinity;
	
 	public int damagePerShot = 20;

	protected string weaponName = "none";
	public Transform startRay;
	public GameObject impact;

	// Use this for initialization
	private Dictionary<string, bool> contraints;
	protected bool canFire = true;
	protected int magazine;

	protected string[] collisionMask = null;
	private float lastShootTime = 0f;


	protected void Start() {
		collisionMask = collisionMask == null ? new string[2]{"Default", "Player"} : collisionMask;
		magazine = magazineMax;
		contraints = new Dictionary<string, bool>();
	}

	protected virtual void shootOnEntity(Script_Entity entity, Vector2 point) {
		entity.hit(damagePerShot, GetComponent<Script_Entity>().entityColor, "weapon", weaponName);
	}

	protected virtual void shootOnTile(Script_TileHandler tile, Vector2 point) {
		tile.getShot(gameObject, "weapon", weaponName);
	}

	protected virtual void shootOnShield(GameObject shield, Vector2 point) {
		shield.GetComponent<Script_Shield>().hit();
	}
	protected virtual void shootOnNothing(RaycastHit2D ray) {

	}
	
	public virtual bool fire() {
		if (canFire
		&& lastShootTime + speedFire <= Time.time
		&& (magazine > 0 || magazineMax == 0)) {
			RaycastHit2D hitInfo = castRay(range);
			if (hitInfo == false) {
				shootOnNothing(hitInfo);
			} else {
				//on TILE
				Script_TileHandler tileHandler = hitInfo.collider.gameObject.GetComponent<Script_TileHandler>();
				if (tileHandler)
					shootOnTile(tileHandler, hitInfo.point);
				//on ENTITY
				Script_Entity entityHandler = hitInfo.collider.gameObject.GetComponent<Script_Entity>();
				if (entityHandler)
					shootOnEntity(entityHandler, hitInfo.point);
				
				//on SHIELD
				if (hitInfo.collider.gameObject.tag == "Player Shield")
					shootOnShield(hitInfo.collider.gameObject, hitInfo.point);
				
				if (impact) {
					GameObject instanciedImpact = Instantiate(impact, hitInfo.point, Quaternion.identity);
					instanciedImpact.GetComponent<SpriteRenderer>().color = GetComponent<Script_Entity>().entityColor;
				}
			}
			magazine--;
			lastShootTime = Time.time;
			if (magazine <= 0 && magazineMax != 0)
				Invoke("reload", reloadTime);
			return true;	
		}
		return false;
	}

	public void forceReloadWithAmount(int ammo) {
		if (ammo >= 0)
			magazine = ammo;
	}

	public virtual void reload() {
		magazine = magazineMax;
	}

	public float getPercentAmmo() {
		if (magazineMax == 0)
			return Mathf.Infinity;
		return (magazine / 1f) / (magazineMax / 1f);
	}

	protected RaycastHit2D castRay(float distance = Mathf.Infinity, string[] mask = null) {
		int layerMask = LayerMask.GetMask(collisionMask);
		return Physics2D.Raycast(startRay.transform.position, transform.up, distance, layerMask);
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
	}

}
