using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Tile_Collider : MonoBehaviour {

	public bool crossableByRay = true;
	void Start () {
		Vector3 pos = transform.localPosition;
		transform.localPosition = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
		if (crossableByRay) {
			gameObject.layer = LayerMask.NameToLayer("IgnoredByRay");
		}
	}
}
