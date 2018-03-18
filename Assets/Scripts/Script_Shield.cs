﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Shield : MonoBehaviour {
	private int maxLife = 4;
	private int life;
	private SpriteRenderer rend;
	private PolygonCollider2D col;

	private Dictionary<string, bool> contraints;
	private bool on = false;

	private bool canShield = true;
	// Use this for initialization
	void Start () {
		life = maxLife;
		rend = GetComponent<SpriteRenderer>();
		col = GetComponent<PolygonCollider2D>();
		if (on) up(); else down();
		contraints = new Dictionary<string, bool>();
	}
	
	// Update is called once per frame
	void Update () {
		if (on) {
			enable(life > 0);
		}
	}

	public void up() {
		if (!canShield)
			return;
		on = true;
		if (life > 0) {
			enable(true);
		}
	}

	public void down() {
		on = false;
		enable(false);
	}

	public void hit() {
		if (life > 0) {
			life--;
			updateColor();
		}
	}

	public void refull(int stack = -1) {
		if (stack == -1)
			life = maxLife;
		else {
			life += stack;
			life = life > maxLife ? maxLife : life;
		}
		updateColor();
		enable(on);
	}

	public bool getState() {
		return on;
	}

	public float getPercentShield() {
		return (life / 1.0f) / maxLife;
	}

	private void updateColor() {
		Color newCol = rend.color;
		newCol.a = getPercentShield();
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

	private void updateCanShield() {
		bool can = true;
		foreach(KeyValuePair<string, bool> contraint in contraints) {
			can = can && contraint.Value;
		}
		canShield = can;
	}

	private void enable(bool active) {
		rend.enabled = active;
		col.enabled = active;
	}

}
