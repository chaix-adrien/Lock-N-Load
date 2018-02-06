using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : MonoBehaviour {
//public
	public GamepadInput.GamePad.Index gamepad;
//private
	private Dictionary<string, float> times = new Dictionary<string, float>();
	private Script_Move moveComp;

	private Script_WeaponWithRay weapon;

	void Start () {
		times.Add("lastShoot", 0f);
		moveComp = GetComponent<Script_Move>();
		weapon = GetComponent<Script_WeaponWithRay>();
	}

	void FixedUpdate () {
		move();
		rotate();
	}

	void Update() {
		checkForFire();
	}

	private bool triggerState = false;
	void checkForFire() {
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, gamepad);
		if (!triggerState
		&& trigger >= triggerDeadZoneIn) {
			weapon.fire();
			triggerState = true;
		} else if (trigger < triggerDeadZoneOut)
			triggerState = false;
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
}
