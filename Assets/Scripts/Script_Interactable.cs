using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Interactable : MonoBehaviour {
	public GameObject whenInteractible = null;

	List<int> atRange;

	
	protected void Start() {
		atRange = new List<int>();
		if (whenInteractible) {
			whenInteractible.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public void canInteractWith(int atRangeID, bool can) {
		if (can && atRange.Contains(atRangeID) == false) {
			atRange.Add(atRangeID);
		}
		if (!can)
			atRange.Remove(atRangeID);
		if (whenInteractible)
			whenInteractible.GetComponent<SpriteRenderer>().enabled = (atRange.Count >= 1);
	}

	public virtual void interactWith(GameObject interactor) {}
}
