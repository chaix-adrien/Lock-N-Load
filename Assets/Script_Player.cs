﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void Update () {
		GetComponent<Rigidbody2D>().AddForce(GamePad.GetAxis(GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.Any)* 10);
	}
	
	
}
