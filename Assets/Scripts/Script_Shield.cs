using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Shield : MonoBehaviour {
	private int maxLife = 4;
	private int life;
	private SpriteRenderer rend;
	private PolygonCollider2D col;
	private bool on = true;
	// Use this for initialization
	void Start () {
		life = maxLife;
		rend = GetComponent<SpriteRenderer>();
		col = GetComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (on) {
			gameObject.SetActive(life > 0);
		}
	}

	public void up() {
		on = true;
		if (life > 0)
			gameObject.SetActive(true);
	}

	public void down() {
		on = false;
		gameObject.SetActive(false);
	}

	public void hit() {
		if (life > 0) {
			life--;
			updateColor();
		}
	}

	public void refull() {
		life = maxLife;
		updateColor();
		gameObject.SetActive(on);
	}

	public bool getState() {
		return on;
	}

	private void updateColor() {
		Color newCol = rend.color;
		newCol.a = (life / 1.0f) / maxLife;
		rend.color = newCol;
	}
}
