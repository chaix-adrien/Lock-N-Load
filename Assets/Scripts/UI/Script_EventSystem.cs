using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XboxCtrlrInput;


public class Script_EventSystem : MonoBehaviour {
	private GameObject lastSelec = null;
	void Update () {
		if (EventSystem.current.currentSelectedGameObject == null)
			EventSystem.current.SetSelectedGameObject(lastSelec);
		if (EventSystem.current.currentSelectedGameObject != lastSelec && lastSelec != null) {
			XCIextention.SetVibration((XboxController)1, 0.1f, 0.1f);
		}
		lastSelec = EventSystem.current.currentSelectedGameObject;
	}
}
