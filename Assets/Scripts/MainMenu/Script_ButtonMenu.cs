using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_ButtonMenu : MonoBehaviour {
	public List<GameObject> buttons;
	public int defaultSelection = -1;

	private int selection;
	void Start () {
		buttons = new List<GameObject>();
		selection = defaultSelection;
		for (int i = 0; i < transform.childCount; i++) {
			buttons.Add(transform.GetChild(i).gameObject);
		}
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
		if (Input.GetKeyDown("return") || GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any)) {
			buttons[selection].GetComponent<Script_ButtonMenu_Button>().OnActivate();
		}

	}

	public void unselectObject(GameObject obj) {
		selection = -1;
		obj.SendMessage("OnUnselect", 0f);
	}

	public void selectObject(GameObject obj) {
		for (int i = 0; i < buttons.Count; i++) {
			if (buttons[i] != obj)
				buttons[i].SendMessage("OnUnselect", 0f);
			else
				selection = i;
		}
		obj.SendMessage("OnSelect", 0f);
	}
}
