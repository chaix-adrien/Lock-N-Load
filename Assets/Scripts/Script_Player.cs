using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : MonoBehaviour {
//public
	public GamepadInput.GamePad.Index gamepad;
	public float speed;
//private
	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
		move();
		rotate();


	}

	void rotate() {
		Vector2 rightStick = GamePad.GetAxis(GamePad.Axis.RightStick, gamepad);
		float angle = Vector2.SignedAngle(new Vector2(0, 1), rightStick);
		transform.localEulerAngles = new Vector3(0, 0, angle);
	}

	void move() {
		Vector2 leftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, gamepad);
		rb.velocity = leftStick * speed;
	}
	
    void OnCollisionEnter2D(Collision2D col){
	}

	void OnTriggerEnter2D(Collider2D col) {
	}
}
