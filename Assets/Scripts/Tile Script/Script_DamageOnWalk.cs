using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DamageOnWalk : Script_TileHandler {

	// Use this for initialization

	public int damages = 10;

	public Color onHitColor = Color.white;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void walkedOnStay(Collider2D col) {
		Script_Entity entity = col.gameObject.GetComponent<Script_Entity>();
		if (!col.isTrigger && entity) {
			entity.hit(damages, onHitColor);
		}
	}	
}
