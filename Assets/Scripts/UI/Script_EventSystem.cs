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
		if (XCI.GetButtonDown(XboxButton.A)) {
			if (EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().GetType() == typeof(Slider)) {
				Selectable current = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
				EventSystem.current.sendNavigationEvents = false;
				if (current.FindSelectableOnRight())
					EventSystem.current.SetSelectedGameObject(current.FindSelectableOnRight().gameObject);
				else if (current.FindSelectableOnDown())
					EventSystem.current.SetSelectedGameObject(current.FindSelectableOnDown().gameObject);
				Invoke("resetEvent", 0.1f);
			}
				
		}
		if (EventSystem.current.currentSelectedGameObject != lastSelec && lastSelec != null) {
			XCIextention.SetVibration((XboxController)1, 0.1f, 0.1f);
		}
		lastSelec = EventSystem.current.currentSelectedGameObject;
	}

	private void resetEvent() {
		EventSystem.current.sendNavigationEvents = true;
	}
}
