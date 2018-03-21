using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DestroySelfWithTime : MonoBehaviour {
	public float destroyTime = 1f;
	public bool autoFade = false;

	private float initTime;
	// Use this for initialization
	void Start () {
		Invoke("destroySelf", destroyTime);
		initTime = Time.time;
	}
	
	void Update() {
		if (autoFade) {
			Color faded = GetComponent<SpriteRenderer>().color;
			faded.a = 1f - (Time.time - initTime) / destroyTime;
			GetComponent<SpriteRenderer>().color = faded;
		}
	}

	void destroySelf() {
		Destroy(gameObject);
	}
}
