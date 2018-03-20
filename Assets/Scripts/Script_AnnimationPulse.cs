using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_AnnimationPulse : MonoBehaviour {
	public float ratio = 0.5f;
	public float speed = 2f;
	// Update is called once per frame
	void Update () {
		float size = Mathf.Sin(Time.time * speed) * ratio;
		transform.localScale = new Vector3(1 + size, 1 + size, 1);
	}
}
