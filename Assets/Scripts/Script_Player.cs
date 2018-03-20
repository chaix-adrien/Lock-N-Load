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
	public float interactionRange = 0.25f;
	public GameObject impactSprite;
	public GameObject shieldObject;
	public Script_Cut cut;
//private
	private Script_Interactable canInteractWith;
	private Script_Move moveComp;

	private Script_WeaponWithRay weapon;
	private Script_Shield shield;

	new void Start () {
		base.Start();
		moveComp = GetComponent<Script_Move>();
		weapon = GetComponent<Script_WeaponWithRay>();
		shieldObject.GetComponent<SpriteRenderer>().color = entityColor;
		shield = shieldObject.GetComponent<Script_Shield>();
		cut = GetComponent<Script_Cut>();
	}

	void FixedUpdate () {
		move();
		rotate();
	}

	protected new void Update() {
		base.Update();
		checkForShield();
		checkForFire();
		checkForCut();
		checkForAction();
	}

	bool triggerStateRight = false;
	void checkForFire() {
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = 0f;
		if (controllMode == moveMode.CONTROLLER)
			trigger = GamePad.GetTrigger(GamePad.Trigger.RightTrigger, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			trigger = Input.GetKey("space") ? 1f : 0f;
		if (!triggerStateRight
		&& trigger >= triggerDeadZoneIn) {
			weapon.fire();
			triggerStateRight = true;
		} else if (trigger < triggerDeadZoneOut)
			triggerStateRight = false;
	}

	private bool triggerStateLeft = false;
	void checkForShield() {
		
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = 0f;
		if (controllMode == moveMode.CONTROLLER)
			trigger = GamePad.GetTrigger(GamePad.Trigger.LeftTrigger, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			trigger = Input.GetKey("x") ? 1f : 0f;
		if (!triggerStateLeft
		&& trigger >= triggerDeadZoneIn) {
			shield.up();
			triggerStateLeft = true;
			weapon.addContraint("shield", false);
			cut.addContraint("shield", false);
		} else if (trigger < triggerDeadZoneOut && shield.getState() == true) {
			weapon.removeContraint("shield");
			cut.removeContraint("shield");
			shield.down();
		}
	}

	private void checkForCut() {
		bool doIt = false;
		if (controllMode == moveMode.CONTROLLER)
			doIt = GamePad.GetButtonDown(GamePad.Button.B, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			doIt = Input.GetKeyDown("c");
		if (doIt) {
			cut.fire();
		}
	}

	private void checkForAction() {
		bool doIt = false;
		if (controllMode == moveMode.CONTROLLER)
			doIt = GamePad.GetButtonDown(GamePad.Button.X, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			doIt = Input.GetKeyDown("a");
		if (doIt) {
			action();
		}
	}

	private void action() {
		if (canInteractWith) {
			canInteractWith.interactWith(gameObject);
		}
	}

	void rotate() {
		if (cut.getState())
			return;
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

	protected override void onHit(int damages, Color hitColor, string from) {
		if (from == "weapon") {
			float impactTime = 1f;
			impactSprite.GetComponent<SpriteRenderer>().color = hitColor;
			Invoke("hideImpactSprite", impactTime);
		}
	}

	private void hideImpactSprite() {
		impactSprite.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
	}

	protected override void die() {
		Debug.Log("player get dead");
	}

	void OnTriggerEnter2D(Collider2D col) {
		Script_Interactable interaction = col.gameObject.GetComponent<Script_Interactable>();
		if (interaction) {
			canInteractWith = interaction;
			interaction.canInteractWith(GetInstanceID(), true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		Script_Interactable interaction = col.gameObject.GetComponent<Script_Interactable>();
		if (interaction && canInteractWith == interaction)	
			canInteractWith = null;
		if (interaction)
			interaction.canInteractWith(GetInstanceID(), false);
	}
}
