using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : Script_Entity {
//public
	 [Header("Player parameters")]
	public GamepadInput.GamePad.Index gamepad;

		public enum moveMode {
		KEYBOARD,
		CONTROLLER,
	};

	public moveMode controllMode = moveMode.CONTROLLER;
//private
	private Dictionary<string, float> times = new Dictionary<string, float>();
	private Script_Move moveComp;

	private Script_WeaponWithRay weapon;

	void Start () {
		base.Start();
		times.Add("lastShoot", 0f);
		moveComp = GetComponent<Script_Move>();
		weapon = GetComponent<Script_WeaponWithRay>();
	}

	void FixedUpdate () {
		move();
		rotate();
	}

	protected void Update() {
		base.Update();
		checkForFire();
	}

	private bool triggerState = false;
	void checkForFire() {
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = 0f;
		if (controllMode == moveMode.CONTROLLER) {
			trigger = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, gamepad);
		} else if (controllMode == moveMode.KEYBOARD) {
			trigger = Input.GetKey("space") ? 1f : 0f;
		}
		if (!triggerState
		&& trigger >= triggerDeadZoneIn) {
			weapon.fire();
			triggerState = true;
		} else if (trigger < triggerDeadZoneOut)
			triggerState = false;
	}

	
	void rotate() {
		Vector2 rightStick = new Vector2(0f, 0f);
		if (controllMode == moveMode.CONTROLLER) {
			rightStick = GamePad.GetAxis(GamePad.Axis.RightStick, gamepad);
		} else if (controllMode == moveMode.KEYBOARD) {
			rightStick.x = Input.GetAxis("horizontal_arrow");
			rightStick.y = Input.GetAxis("vertical_arrow");
		}
		if (rightStick.magnitude >= 0.1) {
			float angle = Vector2.SignedAngle(new Vector2(0, 1), rightStick);
			transform.localEulerAngles = new Vector3(0, 0, angle);
		}
	}

	void move() {
		Vector2 leftStick = new Vector2(0f, 0f);
		if (controllMode == moveMode.CONTROLLER) {
			leftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, gamepad);
		}
		else if (controllMode == moveMode.KEYBOARD) {
			leftStick.x = Input.GetAxis("horizontal");
			leftStick.y = Input.GetAxis("vertical");
		}
		moveComp.move(leftStick);
	}

	protected override void die() {
		Debug.Log("player get dead");
	}
}
