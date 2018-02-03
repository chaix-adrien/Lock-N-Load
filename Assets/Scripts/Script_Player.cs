using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : MonoBehaviour {
//public
	public GamepadInput.GamePad.Index gamepad;

	public GameObject ray;
	public float speed;
	public Transform startRay;
//private
	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		ray.GetComponent<LineRenderer>().useWorldSpace = true;
	}

	void FixedUpdate () {
		move();
		rotate();
	}

	void Update() {
		displayRay();
	}

	void displayRay() {
		RaycastHit2D hitInfo = Physics2D.Raycast(startRay.transform.position, transform.up);
		ray.GetComponent<LineRenderer>().SetPosition(0, startRay.transform.position);
		if (hitInfo) {
			ray.GetComponent<LineRenderer>().SetPosition(1, hitInfo.point);
		}
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
