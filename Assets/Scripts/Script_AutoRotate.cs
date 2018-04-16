using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AutoRotate : MonoBehaviour {
	public float rotateSpeed = 1f;
	void Update () {
		transform.Rotate(0, 0, rotateSpeed);
	}
}
