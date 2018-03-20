using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Interactable : MonoBehaviour {
	public GameObject whenInteractible = null;

	private GameObject interactIcon;

	List<int> atRange;

	
	protected void Start() {
		atRange = new List<int>();
		if (whenInteractible == null) {
			whenInteractible = Resources.Load("Interactible") as GameObject;
		}
		interactIcon = Instantiate(whenInteractible, Vector3.zero, Quaternion.identity);
		interactIcon.transform.SetParent(transform, false);
		interactIcon.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void canInteractWith(int atRangeID, bool can) {
		if (can && atRange.Contains(atRangeID) == false) {
			atRange.Add(atRangeID);
		}
		if (!can)
			atRange.Remove(atRangeID);
		interactIcon.GetComponent<SpriteRenderer>().enabled = (atRange.Count >= 1);
	}

	public virtual void interactWith(GameObject interactor) {}
}
