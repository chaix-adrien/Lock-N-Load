using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Entity : MonoBehaviour {
	 [Header("Life parameters")]
	public int maxLife = 100;
	public int startLife = 100;
	public int minLife = 0;

	public float invicibleFrame = 1.0f;

	public GameObject lifeBar;

	private int life;
	private float lastShootTime = 0.0f;

	private SpriteRenderer rend;

	// Use this for initialization
	protected void Start () {
		life = startLife;
		rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float elapsedTime = Time.time - lastShootTime;
		if (elapsedTime >= invicibleFrame) {
			displayInvincibleFrame(elapsedTime, elapsedTime / invicibleFrame);
		}
	}

	protected virtual void displayInvincibleFrame(float elapsedTime, float percent) {

	}

	protected virtual void die() {
		Debug.Log("entity get dead");
	}

	public void hit(int damages) {
		Debug.Log("hit " + (Time.time - lastShootTime));
		if (Time.time - lastShootTime >= invicibleFrame) {
			lastShootTime = Time.time;
			life -= damages;
			if (life <= minLife) {
				life = minLife;
				die();
			}
			Debug.Log(life);
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
