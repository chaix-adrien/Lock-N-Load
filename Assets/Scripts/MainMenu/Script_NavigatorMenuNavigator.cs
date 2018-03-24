using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;

public class Script_NavigatorMenuNavigator : MonoBehaviour {
	private List<Script_ButtonMenuNavigation> menu;
	public int defaultSelection = 0;

	private int selection;
	void Start () {
		menu = new List<Script_ButtonMenuNavigation>();
		GetComponentsInChildren<Script_ButtonMenuNavigation>(menu);
		selection = defaultSelection;
		selectObject(menu[selection]);
	}
	
	private bool stickState = false;
	void Update () {
		float leftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).x;
		int toAdd = 0;
		if (Mathf.Abs(leftStick) >= 0.8f && !stickState) {
			stickState = true;
			toAdd = (leftStick < 0) ? -1 : 1;
		} else if (Mathf.Abs(leftStick) <= 0.2f && stickState)
			stickState = false;
		if (Input.GetKeyDown("left") || Input.GetKeyDown("q"))
			toAdd = -1;
		if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
			toAdd = 1;
		
		if (!(selection == -1 && toAdd == 0)) {
			
			selection += toAdd;
			if (selection < 0)
				selection = 0;
			if (selection >= menu.Count)
				selection = menu.Count - 1;
		}
		if (toAdd != 0) {
			for (int i = 0; i < menu.Count; i++) {
				if (i == selection)
					selectObject(menu[i]);
			}
		}
	}

	public void selectObject(Script_ButtonMenuNavigation obj) {
		for (int i = 0; i < menu.Count; i++) {
			if (menu[i] == obj) {
				selection = i;
				obj.enabled = true;
				obj.selectDefault();
			} else
				menu[i].enabled = false;
		}
	}
}
