using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Move : MonoBehaviour {

	public float speed = 1.0f;

	private Rigidbody2D rb;
	private float realSpeed;
	private Dictionary<string, float> contraints;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		realSpeed = speed;
		contraints = new Dictionary<string, float>();
	}
	
	public void move(Vector2 moveVec) {
		if (rb)
			rb.velocity = moveVec * realSpeed;
	}

	public void addContraint(string name, float contraint) {
		if (contraints.ContainsKey(name)) {
			contraints[name] = contraint;
		} else {
			contraints.Add(name, contraint);
		}
		calculRealSpeed();
	}

	void calculRealSpeed() {
		float bigContraint = 1.0f;
		foreach(KeyValuePair<string, float> contraint in contraints) {
			if (contraint.Value < bigContraint)
				bigContraint = contraint.Value;
		}
		realSpeed = speed * bigContraint;
	}

	public void removeContraint(string name) {
		contraints.Remove(name);
		calculRealSpeed();
	}

	public void setSpeed(float speedCoef) {
		realSpeed = speed * speedCoef;	
	}
}
