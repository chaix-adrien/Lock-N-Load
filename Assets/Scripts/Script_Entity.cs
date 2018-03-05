﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Entity : MonoBehaviour {
	 [Header("Life parameters")]
	public int maxLife = 100;
	public int startLife = 100;
	public int minLife = 0;

	public float invicibleFrame = 1.0f;

	public Color invicibleFrameColor = Color.white;

	public GameObject lifeBar;

	private int life;
	private float lastShootTime;

	private SpriteRenderer rend;
	public Color entityColor;

	// Use this for initialization
	protected void Start () {
		life = startLife;
		rend = GetComponent<SpriteRenderer>();
		lastShootTime = -invicibleFrame;
		GetComponent<SpriteRenderer>().color = entityColor;
		
	}
	
	// Update is called once per frame
	protected void Update () {
		float elapsedTime = Time.time - lastShootTime;
		if (elapsedTime < invicibleFrame) {
			displayInvincibleFrame(elapsedTime, elapsedTime / invicibleFrame);
		}
	}

	protected virtual void displayInvincibleFrame(float elapsedTime, float percent) {
		if (percent >= 0.9)
			rend.color = entityColor;
		else if (elapsedTime % 0.5 < 0.25) 
			rend.color = invicibleFrameColor;
		else
			rend.color = entityColor;
	}

	protected virtual void die() {
		Debug.Log("entity get dead");
	}

	public void hit(int damages, Color hitColor) {
		if (Time.time - lastShootTime >= invicibleFrame) {
			invicibleFrameColor = hitColor;
			lastShootTime = Time.time;
			life -= damages;
			if (life <= minLife) {
				life = minLife;
				die();
			}
			if (lifeBar)
				lifeBar.GetComponent<Script_LifeBar>().refreshLifeBar(getPercentLife());
		}
	}

	public void heal(int regen) {
		life += regen;
		if (life > maxLife)
			life = maxLife;
	}

	public float getPercentLife() {
		return (life / (maxLife / 1.0f));
	}
}