using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_RandomRotateOnAxis : MonoBehaviour {
	void Start () {
		var rot = Mathf.RoundToInt(Random.Range(0f, 4f)) * 90;
		transform.Rotate(0, 0, rot);
	}
}
