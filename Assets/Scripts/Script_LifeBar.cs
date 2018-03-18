using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_LifeBar : MonoBehaviour {	
	public void refreshLifeBar(float percent) {
		transform.localScale = new Vector3(percent, 1f, 1f);
	}
}
