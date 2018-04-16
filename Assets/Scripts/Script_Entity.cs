using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Entity : MonoBehaviour {
	 [Header("Life parameters")]
	public int maxLife = 100;
	public int startLife = 100;
	public int minLife = 0;

	public float invicinbleFrame = 1.0f;

	private float invincibleFrameFade = 0.5f;

	public GameObject lifeBar;

	protected bool alive = true;
	protected int life;
	private float invincibleUntil = -1;
	private float invincibleSince = -1;

	private SpriteRenderer rend;
	public Color entityColor;
	public string entityName = "none";

	// Use this for initialization
	protected void Start () {
		life = startLife;
		rend = GetComponent<SpriteRenderer>();
		GetComponent<SpriteRenderer>().color = entityColor;
		
	}
	
	// Update is called once per frame
	protected void Update () {
		if (!alive)
			return;
		if (Time.time <= invincibleUntil) {
			float elapsedTime = Time.time - invincibleSince;
			float totalTime = invincibleUntil - invincibleSince;
			displayInvincibleFrame(elapsedTime, elapsedTime / totalTime);
		}
	}

	protected virtual void displayInvincibleFrame(float elapsedTime, float percent) {
		if (!alive)
			return;
		Color faded = entityColor;
		faded.a = invincibleFrameFade;
		if (percent >= 0.9)
			rend.color = entityColor;
		else if (elapsedTime % 0.5 < 0.25) 
			rend.color = faded;
		else
			rend.color = entityColor;
	}

	public bool isAlive() {
		return alive;
	}

	protected virtual void die(int damages, Color hitColor, string from, string fromDetails, GameObject fromObject) {
		alive = false;
		gameObject.SetActive(false);
	}

	public virtual void respawn() {
		alive = true;
		heal(maxLife);
		transform.localScale = new Vector3(1, 1, 1);
		gameObject.SetActive(true);
	}

	protected virtual void onHit(int damages, Color hitColor, string from, string fromDetails, GameObject fromObject) {
	}

	public void hit(int damages, Color hitColor, string from, string fromDetails, GameObject fromObject) {
		if (!alive)
			return;
		if (damages > 0 && Time.time > invincibleUntil) {
			turnInvincible(invicinbleFrame);
			life -= damages;
			if (life <= minLife) {
				life = minLife;
				die(damages, hitColor, from, fromDetails, fromObject);
			}
			if (lifeBar)
				lifeBar.GetComponent<Script_LifeBar>().refreshLifeBar(getPercentLife());
			onHit(damages, hitColor, from, fromDetails, fromObject);
		}
	}

	public void turnInvincible(float time) {
		invincibleSince = Time.time;
		invincibleUntil = invincibleSince + time;
	}

	public void heal(int regen) {
		if (!alive)
			return;
		life += regen;
		if (life > maxLife)
			life = maxLife;
		if (lifeBar)
				lifeBar.GetComponent<Script_LifeBar>().refreshLifeBar(getPercentLife());
	}

	public float getPercentLife() {
		return (life / (maxLife / 1.0f));
	}
}
