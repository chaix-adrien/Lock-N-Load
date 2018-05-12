using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AnnimationPulse : MonoBehaviour {
	public float ratio = 0.5f;
	public float speed = 2f;
	private Vector3 start;
	void Start() {
		start = transform.localScale;
	}
	void Update () {
		float size = Mathf.Sin(Time.time * speed) * ratio;
		transform.localScale = new Vector3(start.x + size, start.y + size, 1);
	}
}
