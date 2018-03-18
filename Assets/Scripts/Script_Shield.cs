using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Shield : MonoBehaviour {
	private int maxLife = 4;
	private int life;
	private SpriteRenderer rend;

	private Dictionary<string, bool> contraints;
	private bool on = false;

	private bool canShield = true;
	// Use this for initialization
	void Start () {
		life = maxLife;
		rend = GetComponent<SpriteRenderer>();
		if (on) up(); else down();
		contraints = new Dictionary<string, bool>();
	}
	
	// Update is called once per frame
	void Update () {
		if (on) {
			gameObject.SetActive(life > 0);
		}
	}

	public void up() {
		if (!canShield)
			return;
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

	public void addContraint(string name, bool enableShield) {
		if (contraints.ContainsKey(name)) {
			contraints[name] = enableShield;
		} else {
			contraints.Add(name, enableShield);
		}
		updateCanShield();
	}

	public void removeContraint(string name) {
		contraints.Remove(name);
		updateCanShield();
	}

	void updateCanShield() {
		bool can = true;
		foreach(KeyValuePair<string, bool> contraint in contraints) {
			can = can && contraint.Value;
		}
		canShield = can;
	}

}
