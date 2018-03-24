using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_DestroySelfWithTime : MonoBehaviour {
	public bool justDisable = false;
	public float destroyTime = 1f;
	public bool autoFade = false;

	private float initTime;
	// Use this for initialization
	void OnEnable () {
		Invoke("destroySelf", destroyTime);
		initTime = Time.time;
	}
	
	private Color recuptColor() {
		Color faded = Color.black;
		if (GetComponent<SpriteRenderer>())
			faded = GetComponent<SpriteRenderer>().color;
		else if (GetComponent<Image>())
			faded = GetComponent<Image>().color;
		else if (GetComponent<Text>())
			faded = GetComponent<Text>().color;
		return faded;
	}

	void Update() {
		if (autoFade) {
			Color faded = recuptColor();
			faded.a = 1f - (Time.time - initTime) / destroyTime;
			applyColor(faded);
		}
	}

	private void applyColor(Color faded) {
		if (GetComponent<SpriteRenderer>())
			faded = GetComponent<SpriteRenderer>().color = faded;
		else if (GetComponent<Image>())
			faded = GetComponent<Image>().color = faded;
		else if (GetComponent<Text>())
			faded = GetComponent<Text>().color = faded;
	}

	void destroySelf() {
		if (justDisable)
			gameObject.SetActive(false);
		else
			Destroy(gameObject);
	}
}
