using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;
public class Script_Player : Script_Entity {
//public
	 [Header("Player parameters")]
	//public GamepadInput.GamePad.Index gamepad;
	public XboxController gamepad;

	public enum moveMode {
		KEYBOARD,
		CONTROLLER,
	};
	public moveMode controllMode = moveMode.CONTROLLER;
	public float interactionRange = 0.25f;
	public GameObject impactSprite;
	public GameObject shieldObject;
	public Script_Cut cut;
	public GameObject killField;
	public AudioClip onEnvironementHit;
	private int kill = 0;
	private int score = 0;
//private
	private Script_Interactable canInteractWith;
	private Script_Move moveComp;

	private Script_WeaponWithRay weapon;
	private Script_Shield shield;

	new void Start () {
		base.Start();
		moveComp = GetComponent<Script_Move>();
		weapon = GetComponent<Script_WeaponWithRay>();
		weapon.ray.GetComponent<Script_Ray>().StartFire = entityColor;
		weapon.ray.GetComponent<Script_Ray>().EndFire = entityColor;
		shieldObject.GetComponent<SpriteRenderer>().color = entityColor;
		shield = shieldObject.GetComponent<Script_Shield>();
		cut = GetComponent<Script_Cut>();
		turnInvincible(2f);
		shield.refull();
	}

	void FixedUpdate () {
		move();
		rotate();
		checkForPause();
		checkForReload();
		checkForShield();
		checkForFire();
		checkForCut();
		checkForAction();
	}

	void checkForPause() {
		if (controllMode == moveMode.CONTROLLER && XCI.GetButtonDown(XboxButton.Start, gamepad)) {
			GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<bl_PauseMenu>().DoPause();
		}
	}

	bool triggerStateRight = false;
	void checkForFire() {
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = 0f;
		if (controllMode == moveMode.CONTROLLER)
			trigger = XCI.GetAxis(XboxAxis.RightTrigger, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			trigger = Input.GetKey("space") ? 1f : 0f;
		if (!triggerStateRight
		&& trigger >= triggerDeadZoneIn) {
			if (weapon.fire())
				gamepad.SetVibration(0.1f, 0.7f);
			triggerStateRight = true;
		} else if (trigger < triggerDeadZoneOut)
			triggerStateRight = false;
	}

	void checkForShield() {
		float triggerDeadZoneIn = 0.9f;
		float triggerDeadZoneOut = 0.5f;
		float trigger = 0f;
		if (controllMode == moveMode.CONTROLLER)
			trigger = XCI.GetAxis(XboxAxis.LeftTrigger, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			trigger = Input.GetKey("x") ? 1f : 0f;
		if (trigger >= triggerDeadZoneIn) {
			if (shield.up()) {
				gamepad.SetVibration(0, 0.1f);
				weapon.addContraint("shield", false);
				cut.addContraint("shield", false);
			}
		} else if (trigger < triggerDeadZoneOut && shield.getState() == true) {
			weapon.removeContraint("shield");
			cut.removeContraint("shield");
			shield.down();
			gamepad.StopVibration();
		}
		
	}

	private void checkForCut() {
		bool doIt = false;
		if (controllMode == moveMode.CONTROLLER)
			doIt = XCI.GetButtonDown(XboxButton.B, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			doIt = Input.GetKeyDown("c");
		if (doIt) {
			if (cut.fire())
				gamepad.SetVibration(0.1f, 0.7f);
		}
	}

	private void checkForReload() {
		bool doIt = false;
		if (controllMode == moveMode.CONTROLLER)
			doIt = XCI.GetButtonDown(XboxButton.X, gamepad);
		else if (controllMode == moveMode.KEYBOARD)
			doIt = Input.GetKeyDown("e");
		if (doIt) {
			weapon.reload();
		}
	}

	private void checkForAction() {
		bool doIt = false;
		if (controllMode == moveMode.CONTROLLER)
			doIt = XCI.GetButtonDown(XboxButton.X, gamepad);
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
			rightStick.y = XCI.GetAxis(XboxAxis.RightStickY, gamepad);
			rightStick.x = XCI.GetAxis(XboxAxis.RightStickX, gamepad);
		} else if (controllMode == moveMode.KEYBOARD) {
			rightStick.x = Input.GetAxis("Horizontal_arrow");
			rightStick.y = Input.GetAxis("Hertical_arrow");
		}
		if (rightStick.magnitude >= 0.1) {
			float angle = Vector2.SignedAngle(new Vector2(0, 1), rightStick);
			transform.localEulerAngles = new Vector3(0, 0, angle);
		}
	}

	void move() {
		Vector2 leftStick = new Vector2(0f, 0f);
		if (controllMode == moveMode.CONTROLLER) {
			leftStick.y = XCI.GetAxis(XboxAxis.LeftStickY, gamepad);
			leftStick.x = XCI.GetAxis(XboxAxis.LeftStickX, gamepad);
		}
		else if (controllMode == moveMode.KEYBOARD) {
			leftStick.x = Input.GetAxis("Horizontal");
			leftStick.y = Input.GetAxis("Vertical");
		}
		moveComp.move(leftStick);
	}

	protected override void onHit(int damages, Color hitColor, string from, string fromDetails, GameObject fromObject) {
		gamepad.SetVibration(0.1f, 1f);
		if (from == "weapon") {
			float impactTime = 1f;
			impactSprite.GetComponent<SpriteRenderer>().color = hitColor;
			Invoke("hideImpactSprite", impactTime);
		} else if (from == "environement") {
			GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onEnvironementHit);
		}
	}

	private void hideImpactSprite() {
		impactSprite.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
	}
	void OnTriggerEnter2D(Collider2D col) {
		Script_Interactable interaction = col.gameObject.GetComponent<Script_Interactable>();
		if (!interaction)
			interaction = col.gameObject.GetComponentInParent<Script_Interactable>();
		if (interaction) {
			canInteractWith = interaction;
			interaction.canInteractWith(GetInstanceID(), true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		Script_Interactable interaction = col.gameObject.GetComponent<Script_Interactable>();
		if (interaction && canInteractWith == interaction)	
			canInteractWith = null;
		if (interaction) {
			interaction.canInteractWith(GetInstanceID(), false);
		}
	}

	protected override void die(int damages, Color hitColor, string from, string fromDetails, GameObject fromObject) {
		var killPanel = Instantiate(killField, transform.position, Quaternion.identity);
		killPanel.transform.SetParent(GameObject.FindGameObjectWithTag("HUD").transform, false);
		killPanel.transform.position = transform.position;
		killPanel.transform.localScale = new Vector3(1, 1, 1);
		var text = killPanel.GetComponentInChildren<Text>();
		var img = killPanel.GetComponentsInChildren<Image>()[1];
		text.text = fromDetails;
		img.sprite = fromObject.GetComponent<SpriteRenderer>().sprite;
		img.color = fromObject.GetComponent<SpriteRenderer>().color;
		gamepad.StopVibration();
		if (fromObject.GetComponent<Script_Player>()) {
			if (fromObject != gameObject)
				fromObject.GetComponent<Script_Player>().addKill();
		}
			
		base.die(damages, hitColor, from, fromDetails, fromObject);
	}

	public int getScore() {return score;}
	public int getKill() {return kill;}

	public void addScore() {score++;}
	public void addKill() {kill++;}
	
	public void Reset(bool resetScore) {
		if (resetScore) {
			score = 0;
			kill = 0;
		}
		shield.refull();
		weapon.reload(true);
		respawn();
	}
}
