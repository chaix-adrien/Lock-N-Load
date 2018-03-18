using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DestructibleTile : Script_TileHandler {
	public Sprite[] spritesState;
	public bool walkableWhenBroken = true;
	public bool rayAccrossWhenBroken = true;
	private int spriteId;
	private SpriteRenderer rend;

	protected override void Start () {
		base.Start();
		spriteId = 0;		
		rend = GetComponent<SpriteRenderer>();
	}

	public override void getShot(GameObject player) {
		if (spriteId < spritesState.Length) {
			rend.sprite = spritesState[spriteId];
			spriteId++;
		}
		if (spriteId >= spritesState.Length) {
			GetComponent<BoxCollider2D>().isTrigger = walkableWhenBroken;
			if (rayAccrossWhenBroken)
				gameObject.layer = LayerMask.NameToLayer("IgnoredByRay");
		}
	}
}
