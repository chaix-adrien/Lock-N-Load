using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Shield : MonoBehaviour {
	public AudioClip onBreakSound;
	public AudioClip onHitSound;
	public int maxLife = 4;
	private int life;
	private SpriteRenderer rend;
	private PolygonCollider2D col;
	private Dictionary<string, bool> contraints;
	private bool on = false;
	private bool canShield = true;

	void Awake() {
		rend = GetComponent<SpriteRenderer>();
		col = GetComponent<PolygonCollider2D>();
	}
	void Start () {
		life = maxLife;
		
		if (on) up(); else down();
		contraints = new Dictionary<string, bool>();
	}
	
	void Update () {
		if (on) {
			enable(life > 0);
		}
	}

	public bool up() {
		if (!canShield || on)
			return false;
		on = true;
		if (life > 0) {
			enable(true);
			return true;
		}
		return false;
	}

	public void down() {
		on = false;
		enable(false);
	}

	public void hit(int charge = 1) {
		if (life > 0) {
			if (charge == -1)
				life = 0;
			else {
				life -= charge;
				life = life < 0 ? 0 : life;
			}
			if (life == 0)
				GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onBreakSound);
			else
				GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onHitSound);				
			updateColor();
		}
	}

	public void refull(int stack = -1) {
		if (stack == -1){			
			life = maxLife;
		}else {
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
