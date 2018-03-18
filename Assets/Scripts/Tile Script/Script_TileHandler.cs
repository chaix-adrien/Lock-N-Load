using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TileHandler : MonoBehaviour {

	protected Vector2Int pos;
	// Use this for initialization
	protected virtual void Start() {
		pos.x = Mathf.FloorToInt(transform.localPosition.x);
		pos.y = Mathf.FloorToInt(transform.localPosition.y);
	}
	
	// Update is called once per frame
	void Update () {}

	protected virtual void walkedOnEnter(Collider2D player) {}

	protected virtual void walkedOnStay(Collider2D player) {}

	protected virtual void walkedOnLeave(Collider2D player) {}

	public virtual void getShot(GameObject player) {}

	void OnTriggerEnter2D(Collider2D col) {
		walkedOnEnter(col);
	}

	void OnTriggerExit2D(Collider2D col) {
		walkedOnLeave(col);
	}

	void OnTriggerStay2D(Collider2D col) {
		walkedOnStay(col);
	}

	
}
