using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;


public class Script_ButtonMenuNavigation : MonoBehaviour {
	private List<Button> buttons;
	public int defaultSelection = -1;

	private int selection;
	void Start () {
		buttons = new List<Button>();
		GetComponentsInChildren<Button>(buttons);
		foreach (var button in buttons) {
			button.gameObject.AddComponent<HighlightFix>();
		}
		selectDefault();
	}
	
	private bool stickState = false;
	void Update () {
		float leftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).y;
		int toAdd = 0;
		if (Mathf.Abs(leftStick) >= 0.8f && !stickState) {
			stickState = true;
			toAdd = (leftStick < 0) ? 1 : -1;
		} else if (Mathf.Abs(leftStick) <= 0.2f && stickState)
			stickState = false;
		if (Input.GetKeyDown("up") || Input.GetKeyDown("z"))
			toAdd = -1;
		if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
			toAdd = 1;
		if (!(selection == -1 && toAdd == 0)) {
			selection += toAdd;
			if (selection < 0)
				selection = 0;
			if (selection >= buttons.Count)
				selection = buttons.Count - 1;
		}
		if (toAdd != 0) {
			for (int i = 0; i < buttons.Count; i++) {
				if (i == selection)
					selectObject(buttons[i]);
			}
		}
	}

	public void selectObject(Button obj) {
		for (int i = 0; i < buttons.Count; i++) {
			if (buttons[i] == obj) {
				selection = i;
				obj.Select();
			}
		}
	}

	public void selectDefault() {
		selection = defaultSelection;
		selectObject(buttons[selection]);
	}
}
