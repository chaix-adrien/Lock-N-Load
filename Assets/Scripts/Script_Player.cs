using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("start");
	}

	void Update () {
		GetComponent<Rigidbody2D>().AddForce(GamePad.GetAxis(GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.Any)* 10);
	}
	
    void OnCollisionEnter2D(Collision2D col){
	}

	void OnTriggerEnter2D(Collider2D col) {
	}
}
