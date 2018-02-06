using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : MonoBehaviour {
//public
	public GamepadInput.GamePad.Index gamepad;

	public GameObject ray;
	public Transform startRay;

	public float reloadTime = 2f;
	public int magazineMax = 3;
	public float speedFire = 0.0f;
//private
	private Dictionary<string, float> times = new Dictionary<string, float>();

	private int magazine;

	private Script_Move moveComp;

	void Start () {
		magazine = magazineMax;
		times.Add("lastShoot", 0f);
		moveComp = GetComponent<Script_Move>();
	}

	void FixedUpdate () {
		move();
		rotate();
	}

	void Update() {
		displayRay();
		checkForFire();
	}

	private bool triggerState = false;
	void checkForFire() {
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, gamepad);
		if (!triggerState
		&& trigger >= triggerDeadZoneIn
		&& times["lastShoot"] + speedFire <= Time.time
		&& magazine > 0) {
			fire();
			times["lastShoot"] = Time.time;
			triggerState = true;
		} else if (trigger < triggerDeadZoneOut)
			triggerState = false;
	}

	void fire() {
		ray.GetComponent<Script_Ray>().fire();
		magazine--;
		if (magazine <= 0) {
			Invoke("reload", reloadTime);
			ray.GetComponent<Script_Ray>().reload();
		}
	}

	void reload() {
		magazine = magazineMax;
		ray.GetComponent<Script_Ray>().endReload();
	}

	void displayRay() {
		int layerMask = 1 << LayerMask.NameToLayer("Default");
		RaycastHit2D hitInfo = Physics2D.Raycast(startRay.transform.position, transform.up, Mathf.Infinity, layerMask);
		ray.GetComponent<LineRenderer>().SetPosition(0, startRay.transform.position);
		if (hitInfo) {
			ray.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
		}
	}

	void rotate() {
		Vector2 rightStick = GamePad.GetAxis(GamePad.Axis.RightStick, gamepad);
		if (rightStick.magnitude >= 0.1) {
			float angle = Vector2.SignedAngle(new Vector2(0, 1), rightStick);
			transform.localEulerAngles = new Vector3(0, 0, angle);
		}
	}

	void move() {
		Vector2 leftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, gamepad);
		moveComp.move(leftStick);
	}
	public float getPercentAmmo() {
		return (magazine / 1f) / (magazineMax / 1f);
	}
}
